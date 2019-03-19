using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApp
{
    public static class SettingsSaver
    {
        public static void SaveFormSettings(Form form, Type formType)
        {
            var grids = GetAllControlsByType(form, typeof(DataGridView));
            Size formSize;
            Point formLocation;
            if (form.WindowState == FormWindowState.Maximized)
            {
                formSize = form.RestoreBounds.Size;
                formLocation = form.RestoreBounds.Location;
            }
            else
            {
                formSize = form.Size;
                formLocation = form.Location;
            }

            var gridsInfo = GetGridsWidthInfo(grids);
            
         //   SaveToXml(formType.Namespace +"."+ form.Name, formSize, formLocation, gridsInfo);

            var info = new FormInfo
            {
                FormName = form.Name,
                FormNamespace = formType.Namespace,
                FormWidth = formSize.Width,
                FormHeight = formSize.Height,
                LocationX = formLocation.X,
                LocationY = formLocation.Y,
                GridsInfos = gridsInfo
            };

            SaveToXml(info,true);
        }

        public static void ShowFormInfo(FormInfo formInfo )
        {
            string text = String.Format($"Form name: {formInfo.FormName}, Form namespace: {formInfo.FormNamespace} width:{formInfo.FormWidth}, height:{formInfo.FormHeight} " +
                                        $", Location: X:{formInfo.LocationX}, Y:{formInfo.LocationY} \n");

            foreach (var grid in formInfo.GridsInfos)
            {
                text += grid.GridName + "\n";
                foreach (var width in grid.GridWidths)
                {
                    text += width.Value+ " ";
                }

                text += Environment.NewLine;
            }
            MessageBox.Show(text);
        }

        private static List<GridsInfo> GetGridsWidthInfo(IEnumerable<Control> grids)
        {
            var gridInfo = new List<GridsInfo>();
            foreach (var control in grids)
            {
                var widthList = new List<SomeInfo<int>>();
                var temp = (DataGridView)control;

                for (var i = 0; i < temp.Columns.Count; i++)
                {
                    widthList.Add(new SomeInfo<int>() {  Value = temp.Columns[i].Width });
                }
                gridInfo.Add( new GridsInfo()
                {
                    GridName = temp.Name,
                    GridWidths = widthList
                });
            }
            return gridInfo;
        }

        public static IEnumerable<Control> GetAllControlsByType(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            var second = controls.ToList();
            return second.SelectMany(ctrl => GetAllControlsByType(ctrl, type))
                .Concat(second)
                .Where(c => c.GetType() == type);
        }


        private static void SaveToXml(FormInfo formInfo, bool indent=false, string settingsFile = "info.xml")
        {
            var serializer = new XmlSerializer(typeof(FormInfo));
            
            // Create an XmlTextWriter using a FileStream.
            Stream fs = new FileStream(settingsFile, FileMode.Create);
            XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings {Indent = indent, Encoding = Encoding.Unicode});
            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, formInfo);
            writer.Close();
        }

        public static FormInfo LoadFromXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(FormInfo));

            // Create an XmlTextWriter using a FileStream.
            StreamReader reader = new StreamReader(fileName);
            // Serialize using the XmlTextWriter.
            var formInfo = (FormInfo)serializer.Deserialize(reader);
            reader.Close();
            return formInfo;
        }

        public static void RestoreFormSettings(Form form, string settingsFile="info.xml")
        {
            FormInfo formInfo = LoadFromXml(settingsFile);

           form.Size = new Size(formInfo.FormWidth, formInfo.FormHeight);
           form.Location = new Point(formInfo.LocationX, formInfo.LocationY);
           SetGridsWidthInfo(form, formInfo.GridsInfos);
           
        }

        private static void SetGridsWidthInfo(Form form, List<GridsInfo> gridsInfos)
        {
            var grids = GetAllControlsByType(form, typeof(DataGridView));
            
            foreach (var control in grids)
            {
                
                var temp = (DataGridView)control;
                var tempGridWidths = gridsInfos.Single(g => g.GridName == temp.Name).GridWidths;

                for (var i = 0; i < temp.Columns.Count; i++)
                {
                    temp.Columns[i].Width = tempGridWidths.First().Value;
                    tempGridWidths.RemoveAt(0);
                }
            }          
        }

        public class SomeInfo<T>
        {
            [XmlAttribute]
            public T Value { get; set; }
        }

        public class FormInfo
        {
            [XmlAttribute]
            public string FormName { get; set; }
            [XmlAttribute]
            public string FormNamespace { get; set; }

            [XmlAttribute]
            public  int FormWidth { get; set; }
            [XmlAttribute]
            public  int FormHeight { get; set; }
            [XmlAttribute]
            public  int LocationX { get; set; }
            [XmlAttribute]
            public int LocationY { get; set; }

            public List<GridsInfo> GridsInfos { get; set; }
        }

        public class  GridsInfo
        {
            [XmlAttribute]
            public string GridName { get; set; }

            [XmlElement("width")]
            public List<SomeInfo<int>> GridWidths { get; set; }
        }
    }
}
