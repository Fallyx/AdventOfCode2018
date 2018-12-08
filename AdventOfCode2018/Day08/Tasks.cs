using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day08
{
    class Tasks
    {
        const string inputPath = @"Day08/Input.txt";
        private static int sumMdate = 0;

        public static void Task1()
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

            LicenseNode root = CreateTreeT1(licenseFile);

            Console.WriteLine(sumMdate);
        }

        private static LicenseNode CreateTreeT1(Stack<string> lF)
        {
            LicenseNode node = new LicenseNode();

            int nrChilds = int.Parse(lF.Pop());
            int nrMdatas = int.Parse(lF.Pop());

            for(int i = 0; i < nrChilds; i++)
            {
                node.Nodes.Add(CreateTreeT1(lF));
            }

            for(int i = 0; i < nrMdatas; i++)
            {
                int mData = int.Parse(lF.Pop());
                node.MetaData.Add(mData);
                sumMdate += mData;
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
