using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace PackageHandler.FactoryModel
{
    public static class KBSystem
    {
        private static IGraph g;
        private static string filePath;

        public static void SetGraph(IGraph graph, string fP)
        {
            g = graph;
            filePath = fP;
        }

        private static void AddNamespaces()
        {
            g.NamespaceMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
            g.NamespaceMap.AddNamespace("rdfs", new Uri("http://www.w3.org/2000/01/rdf-schema#"));
            g.NamespaceMap.AddNamespace("owl", new Uri("http://www.w3.org/2002/07/owls#"));
            g.NamespaceMap.AddNamespace("ch", UriFactory.Create($"http://my.lab.com/characteristic#"));
            g.NamespaceMap.AddNamespace("num", UriFactory.Create($"http://my.lab.com/number#"));
        }
        
        public static void InitGraph()
        {
            g = new Graph();
            AddNamespaces();
            SaveGraph();
        }

        private static void SaveGraph()
        {
            var turtleWriter = new TurtleWriter();
            //Save to a File
            turtleWriter.Save(g, filePath);
        }

        public static bool IfEnoughMark(Mark? mark, out bool canPress)
        {
            var subj = g.CreateUriNode($"ch:{mark.ToString()}");
            var hasNumDamaged = g.CreateUriNode("num:hasNumberOfDamage");
            var hasNumUndamaged = g.CreateUriNode("num:hasNumberOfUnDamage");

            var markTripleDamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumDamaged).FirstOrDefault();
            var markTripleUnDamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumUndamaged).FirstOrDefault();

            if (markTripleDamaged == null || markTripleUnDamaged == null)
            {
                canPress = false;
                return false;
            }

            var ll1 = (markTripleDamaged.Object as LiteralNode);
            int vi = Int32.Parse(ll1.Value);

            var ll2 = (markTripleUnDamaged.Object as LiteralNode);
            int vi2 = Int32.Parse(ll2.Value);

            if (vi + vi2 >= 15)
            {
                if (vi >= vi2) 
                { 
                    canPress = false; 
                }
                else 
                {
                    canPress = true; 
                }
                return true;
            }
            canPress = false;
            return false;
        }

        private static string GetWeightCategory(double weight)
        {
            if (weight < 5)
            {
                return "ExtraLight";
            }
            if (weight < 10)
            {
                return "Light";
            }
            if (weight < 15)
            {
                return "Standard";
            }
            if (weight < 20)
            {
                return "Heavy";
            }
            return "ExtraHeavy";
        }

        public static bool IfEnoughWeight(double weight, out bool canPress)
        {
            var weightCat = GetWeightCategory(weight);
            var subj = g.CreateUriNode($"ch:{weightCat}");
            var hasNumDamaged = g.CreateUriNode("num:hasNumberOfDamage");
            var hasNumUndamaged = g.CreateUriNode("num:hasNumberOfUnDamage");

            var weightTripleDamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumDamaged).FirstOrDefault();
            var weightTripleUndamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumUndamaged).FirstOrDefault();

            if (weightTripleDamaged == null || weightTripleUndamaged == null)
            {
                canPress = false;
                return false;
            }

            var ll1 = (weightTripleDamaged.Object as LiteralNode);
            int vi = Int32.Parse(ll1.Value);

            var ll2 = (weightTripleUndamaged.Object as LiteralNode);
            int vi2 = Int32.Parse(ll2.Value);

            if (vi + vi2 >= 15)
            {
                if (vi >= vi2)
                {
                    canPress = false;
                }
                else
                {
                    canPress = true;
                }
                return true;
            }
            canPress = false;
            return false;
        }

        public static void ModifyPropertyValueCount(double weight, bool isDamaged)
        {
            var weightCat = GetWeightCategory(weight);

            var subj = g.CreateUriNode($"ch:{weightCat}");
            var hasNumDamaged = g.CreateUriNode("num:hasNumberOfDamage");
            var hasNumUndamaged = g.CreateUriNode("num:hasNumberOfUnDamage");

            var weightTripleDamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumDamaged).FirstOrDefault();
            var weightTripleUndamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumUndamaged).FirstOrDefault();

            if (weightTripleDamaged == null && weightTripleUndamaged == null) // Его нет
            {
                //Нет триплета. Создаем новый.
                //ch:Fragile num:hasNumberOfDamage 1 
                var literalValue = g.CreateLiteralNode(isDamaged ? "1" : "0", new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                var triple = new Triple(subj, hasNumDamaged, literalValue);
                g.Assert(triple);

                var literalValue2 = g.CreateLiteralNode(isDamaged ? "0" : "1", new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                var triple2 = new Triple(subj, hasNumUndamaged, literalValue2);
                g.Assert(triple2);
            }
            else // Он есть
            {
                if (isDamaged)
                {
                    var literalNode = (weightTripleDamaged.Object as LiteralNode);

                    int valueInt = Int32.Parse(literalNode.Value);
                    int newValueInt = valueInt + 1;

                    var newNode = g.CreateLiteralNode(newValueInt.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                    var newTriple = new Triple(subj, hasNumDamaged, newNode);
                    g.Retract(weightTripleDamaged);
                    g.Assert(newTriple);
                }
                else
                {
                    var literalNode2 = (weightTripleUndamaged.Object as LiteralNode);

                    int valueInt2 = Int32.Parse(literalNode2.Value);
                    int newValueInt2 = valueInt2 + 1;

                    var newNode2 = g.CreateLiteralNode(newValueInt2.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                    var newTriple2 = new Triple(subj, hasNumUndamaged, newNode2);
                    g.Retract(weightTripleUndamaged);
                    g.Assert(newTriple2);
                }

                SaveGraph();
            }
        }

        public static void ModifyPropertyValueCount(Mark? markNullable, bool isDamaged)
        {
            var mark = markNullable.Value;
         
            var subj = g.CreateUriNode($"ch:{mark.ToString()}");
            var hasNumDamaged = g.CreateUriNode("num:hasNumberOfDamage");
            var hasNumUndamaged = g.CreateUriNode("num:hasNumberOfUnDamage");

            var markTripleDamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumDamaged).FirstOrDefault();
            var markTripleUnDamaged = g.GetTriplesWithSubjectPredicate(subj, hasNumUndamaged).FirstOrDefault();

            if (markTripleDamaged == null && markTripleUnDamaged == null) // Его нет
            {
                //Нет триплета. Создаем новый.
                //ch:Fragile num:hasNumberOfDamage 1 
                var literalValue = g.CreateLiteralNode(isDamaged ? "1" : "0", new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                var triple = new Triple(subj, hasNumDamaged, literalValue);
                g.Assert(triple);

                var literalValue2 = g.CreateLiteralNode(isDamaged ? "0" : "1", new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                var triple2 = new Triple(subj, hasNumUndamaged, literalValue2);
                g.Assert(triple2);
            }
            else // Он есть
            {
                if (isDamaged)
                {
                    var literalNode = (markTripleDamaged.Object as LiteralNode);

                    int valueInt = Int32.Parse(literalNode.Value);
                    int newValueInt = valueInt + 1;

                    var newNode = g.CreateLiteralNode(newValueInt.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                    var newTriple = new Triple(subj, hasNumDamaged, newNode);
                    g.Retract(markTripleDamaged);
                    g.Assert(newTriple);
                }
                else
                {
                    var literalNode2 = (markTripleUnDamaged.Object as LiteralNode);

                    int valueInt2 = Int32.Parse(literalNode2.Value);
                    int newValueInt2 = valueInt2 + 1;

                    var newNode2 = g.CreateLiteralNode(newValueInt2.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
                    var newTriple2 = new Triple(subj, hasNumUndamaged, newNode2);
                    g.Retract(markTripleUnDamaged);
                    g.Assert(newTriple2);
                }
                
                SaveGraph();
            }
        }
    }
}
