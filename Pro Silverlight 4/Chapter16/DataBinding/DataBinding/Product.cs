using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace DataBinding.Local
{
    public class Product : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string modelNumber;
        public string ModelNumber
        {
            get { return modelNumber; }
            set
            {
                modelNumber = value; 
                             
                bool valid = true;
                foreach (char c in modelNumber)
                {
                    if (!Char.IsLetterOrDigit(c))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    List<string> errors = new List<string>();
                    errors.Add("The ModelNumber can only contain letters and numbers.");
                    SetErrors("ModelNumber", errors);
                }
                else
                {
                    ClearErrors("ModelNumber");
                }
                                
                OnPropertyChanged(new PropertyChangedEventArgs("ModelNumber"));
            }
        }

        private string modelName;        
        public string ModelName
        {
            get { return modelName; }
            set
            {
                modelName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelName"));
            }
        }

        private double unitCost;
        public double UnitCost
        {
            get { return unitCost; }
            set
            {
                if (value < 0) throw new ArgumentException("Can't be less than 0.");
                
                unitCost = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UnitCost"));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string productImagePath;
        public string ProductImagePath
        {
            get { return productImagePath; }
            set { productImagePath = value; }
        }

        public Product(string modelNumber, string modelName,
            double unitCost, string description)
        {
            ModelNumber = modelNumber;
            ModelName = modelName;
            UnitCost = unitCost;
            Description = description;
        }

        public Product(string modelNumber, string modelName,
           double unitCost, string description,
           string productImagePath)
            :
           this(modelNumber, modelName, unitCost, description)
        {
            ProductImagePath = productImagePath;
        }

        public Product(string modelNumber, string modelName,
            double unitCost, string description, string categoryName,
            string productImagePath) :
            this(modelNumber, modelName, unitCost, description)
        {
            CategoryName = categoryName;
            ProductImagePath = productImagePath;
        }

        public Product() { }

        public override string ToString()
        {
            return ModelName + " (" + ModelNumber + ")";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private DateTime dateAdded = DateTime.Now;
        public DateTime DateAdded
        {
            get { return dateAdded; }
            set { dateAdded = value; }
        }
                       
        
        // Track all errors. This collection is indexed by property name.
        // Each property can have a colleciton of multiple errors.
        private Dictionary<string, List<string>> errors = 
            new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            // Add the error collection for a property.
            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);

            // Raise the error-notification event.
            if (ErrorsChanged != null) 
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {            
            errors.Remove(propertyName);
            
            // Raise the error-notification event.
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
                
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // Provide all the error collections.
                return (errors.Values);
            }
            else
            {
                // Provice the error collection for the requested property (if it has errors).
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }  
        }

        public bool HasErrors
        {
            get
            {
                // Indicate whether this entire object is error-free.
                return (errors.Count > 0);
            }
        }
    }

}
