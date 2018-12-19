using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Day08
{
    class Tasks
    {
        const string inputPath = @"Day08/Input.txt";
        private static int sumMdate = 0;
        private static int nodeVal = 0;

        public static LicenseNode Task1()
        {
            string input = "";
            Stack<string> licenseFile = new Stack<string>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }

            string[] nums = input.Split(' ');

            for(int i = nums.Length - 1; i >= 0; i--)
            {
                licenseFile.Push(nums[i]);
            }

            LicenseNode root = CreateTree(licenseFile);

            Console.WriteLine(sumMdate);

            return root;
        }

        public static void Task2(LicenseNode root)
        {
            CalcNodeValue(root);

            Console.WriteLine(nodeVal);
        }

        private static LicenseNode CreateTree(Stack<string> lF)
        {
            LicenseNode node = new LicenseNode();

            int nrChilds = int.Parse(lF.Pop());
            int nrMdatas = int.Parse(lF.Pop());

            for(int i = 0; i < nrChilds; i++)
            {
                node.Nodes.Add(CreateTree(lF));
            }

            for(int i = 0; i < nrMdatas; i++)
            {
                int mData = int.Parse(lF.Pop());
                node.MetaData.Add(mData);
                sumMdate += mData;
            }

            return node;
        }

        private static LicenseNode CalcNodeValue(LicenseNode node)
        {
            if(node.Nodes.Count == 0)
            {
                foreach(int mdata in node.MetaData) 
                {
                    nodeVal += mdata;
                }
            }
            else
            {
                foreach(int childIdx in node.MetaData)
                {
                    if (node.Nodes.Count < childIdx) continue;
                    CalcNodeValue(node.Nodes[childIdx - 1]);
                }
            }

            return node;
        }
    }

    class LicenseNode
    {
        private List<LicenseNode> nodes;
        private List<int> metaData;

        public LicenseNode()
        {
            Nodes = new List<LicenseNode>();
            MetaData = new List<int>();
        }

        public List<int> MetaData { get => metaData; set => metaData = value; }
        internal List<LicenseNode> Nodes { get => nodes; set => nodes = value; }
    }
}
