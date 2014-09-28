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
           
            D3DClass d3dClass = new D3DClass();

            try
            {
                d3dClass.InitializeWithForm(form1);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
                throw;
            }
        }
    }
}
