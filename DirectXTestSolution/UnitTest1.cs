using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ch01_01EmptyProject;


namespace DirectXTestSolution
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateSwapChainWithoutException()
        {
            Form1 form1 = System.Windows.Forms.Form1();
           
            D3DClass d3dClass = new D3DClass();
            d3dClass.InitializeWithForm();
        }
    }
}
