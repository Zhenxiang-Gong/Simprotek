using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;

namespace WindowsApplication2
{
	/// <summary>
	/// Summary description for PersistenceManager.
	/// </summary>
	public class PersistenceManager
	{
      private static PersistenceManager persistenceManager;

		private PersistenceManager()
		{
		}

      public static PersistenceManager GetInstance() 
      {
         if (persistenceManager == null) 
         {
            persistenceManager = new PersistenceManager();
         }
         return persistenceManager;
      }

      public bool Open(Form1 form, string fileName)
      {
         bool ok = true;
         Stream stream = null;
         try
         {
            stream = new FileStream(fileName, FileMode.Open);
            SoapFormatter serializer = new SoapFormatter();
            ArrayList items = (ArrayList)serializer.Deserialize(stream);
            this.SetContent(form, items);
         }
         catch (Exception e) 
         {
            ok = false;
            string message = e.ToString(); 
            MessageBox.Show(message, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally 
         {
            stream.Close();
         }
         return ok;
      }

      private void SetContent(Form1 form, ArrayList items)
      {
         IEnumerator e = items.GetEnumerator();
         while (e.MoveNext())
         {
            object obj = e.Current;
            
            if (obj is A)
            {
               A a = (A)obj;
               a.SetObjectData(a.SerializationInfo, a.StreamingContext);

               IEnumerator e2 = a.MyList.GetEnumerator();
               while (e2.MoveNext())
               {
                  B b = (B)e2.Current;
                  b.SetObjectData(b.SerializationInfo, b.StreamingContext);
               }

               IDictionaryEnumerator de = a.MyTable.GetEnumerator();
               while (de.MoveNext())
               {
                  C c = (C)de.Value;
                  c.SetObjectData(c.SerializationInfo, c.StreamingContext);
               }

               form.SetData(a);
            }
         }
      }

      public void Save(Form1 form, string fileName)
      {
         FileStream fs = null;
         try 
         {
            if (File.Exists(fileName)) 
            {
               fs = new FileStream(fileName, FileMode.Open);
            }
            else 
            {
               fs = new FileStream(fileName, FileMode.Create);
            }

            ArrayList toSerializeItems = this.GetContent(form);
            SoapFormatter serializer = new SoapFormatter();
            serializer.Serialize(fs, toSerializeItems);
         }
         catch (Exception e)
         {
            string message = e.ToString(); 
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally 
         {
            if (fs != null) 
            {
               fs.Flush();
               fs.Close();
            }
         }
      }

      private ArrayList GetContent(Form1 form)
      {
         ArrayList toSerializeItems = new ArrayList();
         toSerializeItems.Add(form.a);
         return toSerializeItems;
      }
	}
}
