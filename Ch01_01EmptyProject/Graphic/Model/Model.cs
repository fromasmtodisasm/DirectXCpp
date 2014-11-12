using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    public class Model
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ModelFormat
        {
            public float x, y, z;
            public float tu, tv;
            public float nx, ny, nz;
        }

        public ModelFormat[] Data { get { return modelObject; } }

        string modelFolder = @"D:\GitHub\DirectXCpp\Ch01_01EmptyProject\Graphic\Model";
        private ModelFormat[] modelObject;

        public void LoadModel(string modelName)
        {
            try
            {
                string filePath = String.Format("{0} {1}", modelFolder, modelName);
                List<string> lines = File.ReadLines(filePath).ToList();

                var vertexCountString = lines[0].Split(new char[] { ':' })[1].Trim();
                int vertexCount = int.Parse(vertexCountString);
                modelObject = new ModelFormat[vertexCount];

                int startLine = 4;
                for (int i = startLine; i < lines.Count && i < startLine + vertexCount; i++)
                {
                    var modelArray = lines[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    modelObject[i - startLine] = new ModelFormat()
                    {
                        x = float.Parse(modelArray[0]),
                        y = float.Parse(modelArray[1]),
                        z = float.Parse(modelArray[2]),
                        tu = float.Parse(modelArray[3]),
                        tv = float.Parse(modelArray[4]),
                        nx = float.Parse(modelArray[5]),
                        ny = float.Parse(modelArray[6]),
                        nz = float.Parse(modelArray[7]),
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}