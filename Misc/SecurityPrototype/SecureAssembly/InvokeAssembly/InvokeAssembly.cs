using System;
using System.Reflection; 
using System.Text;
using System.Windows.Forms;

namespace InvokeAssembly
{
	/// <summary>
	/// Interface that would be used by Native Executables to Load Managed Assemblies
	/// </summary>
	public interface IInvokeAssembly
	{
		void LoadAndExecute(byte[] pBuf);
	};

	/// <summary>
	/// CInvokeAssembly implements IInvokeAssembly interface
	/// </summary>
	public class CInvokeAssembly : IInvokeAssembly
	{
		public CInvokeAssembly()
		{
		}
		public void LoadAndExecute(byte[] pBuf)
		{
			try
			{
				Assembly asm = Assembly.Load(pBuf);

				asm.EntryPoint.Invoke(null,null);
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}			
		}
	}
}
