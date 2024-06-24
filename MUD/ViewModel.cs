using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.ComponentModel;
using Autodesk.Revit.DB.IFC;
using System.Net;
using System.Windows;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Policy;





namespace MUD
{
    [Transaction(TransactionMode.Manual)]
    
    class ViewModel : INotifyPropertyChanged, IExternalCommand
    {
        internal static ExternalCommandData commandData;

        public event PropertyChangedEventHandler PropertyChanged;
        public Files SelectedFile { get; set; } //Если выделили объект он будет в актуальном состоянии и можно будет ссылаться на него 

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // URL для скачивания IFC файла
            string fileUrl = "https://disk.yandex.ru/d/J5g0XQqu2OBcsQ";
            // Указываем временный путь для сохранения скачанного файла 
            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(fileUrl));
            try

            {
                // Скачиваем файл с указанного URL

                using (WebClient client = new WebClient())

                {
                    
                    client.DownloadFile(fileUrl, tempFilePath);
                   

                }

                // Получаем текущий документ Revit

                Document doc = commandData.Application.ActiveUIDocument.Document;

                // Начинаем транзакцию для изменений в документе

                using (Transaction tx = new Transaction(doc))

                {

                    tx.Start("Load IFC File");

                    // Путь к IFC файлу

                    ModelPath ifcModelPath = ModelPathUtils.ConvertUserVisiblePathToModelPath(tempFilePath);

                   
                    // Параметры проекта для импорта IFC
                    IFCImportOptions ifcImportOptions = new IFCImportOptions();

                }

                MessageBox.Show("IFC файл успешно загружен и импортирован в модель Revit.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                return Result.Succeeded;

            }

            catch (Exception ex)

            {

                message = "Произошла ошибка: " + ex.Message;

                return Result.Failed;

            }
        }
        public class Files : INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
            private string filePath;

            public bool IsDownload { get; set; }
            public string RevitFileName { get; set; }
            public string FilePath { get => filePath; set { filePath = value; OnPropertyChanged(); } }
            public DateTime ChangeTime { get; set; }
            public string FileName { get; set; }
            public string RevFile { get; set; }
            public string TextDownload { get; set; }

        }
      
    }

}
