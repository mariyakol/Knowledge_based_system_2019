using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using PackageHandler.FactoryModel;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace PackageHandler
{
    public partial class Form1 : Form
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static Random rand = new Random();
        IGraph g;
        string filePath = @"C:\Users\Мария\Desktop\4 курс\проектирование систем\Курсовая работа\PackageHandler\Count.n3";

        private static void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logConsole = new NLog.Targets.ConsoleTarget("console");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);

            LogManager.Configuration = config;
        }

        public Form1()
        {
            InitializeComponent();
            ConfigureLogger();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGraph();
        }

        private void LoadGraph()
        {
            g = new Graph();
            FileLoader.Load(g, filePath);

            KBSystem.SetGraph(g, filePath);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            StartFactory();
        }

        private static void StartFactory()
        {
            logger.Info("Конвейер запущен");
            for (int i = 0; i < 1000; i++)
            {
                var package = new Package();
                logger.Info($"Создана посылка {(package.Barcode == null ? "без штрихкода" : $"со штрихкодом (Press = {package.Barcode.MustPress}, Weight = {package.Barcode.PackageWeight})")}");
                //Thread.Sleep(500);
                ProcessPackage(package);
            }
        }

        private static void ProcessPackage(Package package)
        {
            logger.Info("Обработка посылки:");
            var scanSucceed = Scanner.TryScan(package, out bool canPress);

            //Thread.Sleep(300);
            if (scanSucceed) //Успешный скан штрихкода
            {
                if (!canPress)
                {
                    return;
                }
                Press.DoPress(package);
            }
            else //Неудачный скан
            {
                ProcessIfBarcodeInvalid(package);
            }
        }

        private static void ProcessIfBarcodeInvalid(Package package)
        {
            logger.Info("Начинаю обработку посылки без штрихкода.");
            //Thread.Sleep(300);
            var photo = Camera.TakePhoto(package);
            //Thread.Sleep(300);
            var weight = Scales.GetWeight(package);
            //Thread.Sleep(300);
            var mark = NeuralNetwork.RecognizeMarks(photo);
            //Thread.Sleep(300);

            var isEnoughKnowledge = ExperienceAnalyzer.CanPressIfEnoughKnowledge(weight, mark, out bool canPressFromKB);
            //Thread.Sleep(300);
            if (isEnoughKnowledge) //Хватает знаний
            {
                if (!canPressFromKB)
                {
                    return;
                }

                Press.DoPress(package);
            }
            else //Не хватает знаний
            {
                //Делаем эксперимент
                var isDamaged = ExperimentComponent.TryToPress(package, mark, weight);

                //Thread.Sleep(300);
                logger.Info("Модифицируем базу знаний");

                if (mark.HasValue)
                {
                    KBSystem.ModifyPropertyValueCount(mark, isDamaged);
                }
                else
                {
                    KBSystem.ModifyPropertyValueCount(weight, isDamaged);
                }
            }
        }

        private void NewGraph_Click(object sender, EventArgs e)
        {
            KBSystem.InitGraph();
        }
    }
}
