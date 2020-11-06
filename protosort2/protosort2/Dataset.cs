using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace protosort2
{

    class Dataset : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int[] data;
        private int count;
        private int maxNumber;
        private string filenote;

        public int[] Data
        {
            get => data; set
            {
                data = value;
                NotifyPropertyChanged();
            }
        }
        public int Count
        {
            get => count; set
            {
                count = value;
                NotifyPropertyChanged();
            }
        }
        public int MaxNumber
        {
            get => maxNumber; set
            {
                maxNumber = value;
                NotifyPropertyChanged();
            }
        }
        public string Filenote
        {
            get => filenote; set
            {
                filenote = value;
                NotifyPropertyChanged();
            }
        }

        public Dataset()
        {
            Data = new int[0];
            Count = 0;
            MaxNumber = 0;
            Filenote = "no dataset selected";
        }



        public void GenerateDataset(int count, int maxnumber)
        {
            this.Count = count;
            this.MaxNumber = maxnumber;
            Filenote = "Random Dataset";
            Data = new int[Count];

            var random = new Random();

            for (int i = 0; i < Count; i++)
            {
                Data[i] = random.Next(MaxNumber);
            }
        }


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void ImportCSVFile(string fileText, string fileName)
        {
            try
            {
                string[] items = fileText.Split(',');
                Count = items.Count();
                Data = new int[Count];
                MaxNumber = 0;
                for(int i = 0; i < Count; i++)
                {
                    Data[i] = int.Parse(items[i].Trim());
                    if (Data[i] > MaxNumber) MaxNumber = Data[i];
                }
                Filenote = fileName;
            }
            catch
            {
                Data = new int[0];
                Count = 0;
                MaxNumber = 0;
                Filenote = "Error importing file";
            }
        }

        public string ToCSVString()
        {
            string returnstring = Data[0].ToString();

            for(int i = 1; i < Count; i++)
            {
                returnstring += $", {Data[i]}";
            }
            return returnstring;
        }

        public string ToNewlineString()
        {
            string returnstring = Data[0].ToString();

            for (int i = 1; i < Count; i++)
            {
                returnstring += Environment.NewLine + Data[i].ToString();
            }
            return returnstring;
        }

        public void DeepCopy(Dataset source)
        {
            this.Count = source.Count;
            this.filenote = source.Filenote;
            this.MaxNumber = source.MaxNumber;
            this.Data = new int[Count];
            for (int i = 0; i < count; i++) Data[i] = source.Data[i];
        }
    }
}
