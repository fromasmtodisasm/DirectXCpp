using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ch01_01EmptyProject;
using System.Windows.Forms;


namespace DirectXTestSolution
{
    [TestClass]
    public class D3DClassTest
    {
        [TestMethod]
        public void CreateSwapChainWithoutException()
        {
            Form1 form1 = new Form1();
           
            D3D d3dClass = new D3D();
            WindowConfiguration wc = new  WindowConfiguration();
            wc.Height = 600;
            wc.Width = 800;

            try
            {
                d3dClass.Initialize(form1.Handle, wc);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
                throw;
            }
        }
    }
}
