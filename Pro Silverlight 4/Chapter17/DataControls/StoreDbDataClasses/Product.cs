using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace StoreDbDataClasses
{
    [DataContract(Name = "Product",
        Namespace = "http://www.prosetech.com/StoreDb/Product")]
    [CustomValidation(typeof(ProductValidation), "ValidateProduct")]
    public class Product : INotifyPropertyChanged
    {
        private bool hasChanges = false;
        public bool HasChanges
        {
            get { return hasChanges; }
            set { hasChanges = value; }
        }

        private string modelNumber;
        [DataMember()]
        [StringLength(25)]
        [Display(Name="Model Number",Description= "This is the alphanumeric product tag used in the warehouse.")]
        [Required()]        
        public string ModelNumber
        {
            get { return modelNumber; }
            set
            {                
                // Explicitly raise an exception if a data annotation attribute
                // fails validation.
                ValidationContext context = new ValidationContext(this, null, null);                
                context.MemberName = "ModelNumber";
                Validator.ValidateProperty(value, context);

                modelNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelNumber"));
            }
        }

        private string modelName;
        [DataMember()]
        [Display(Name = "Model Name", Description = "This is the retail product name.")]        
        [Required()]
        [StringLength(Int32.MaxValue,MinimumLength=5)]
        public string ModelName
        {
            get { return modelName; }
            set
            {
                ValidationContext context = new ValidationContext(this, null, null);
                context.MemberName = "ModelName";
                Validator.ValidateProperty(value, context);

                modelName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelName"));
            }
        }

        private double unitCost;
        [DataMember()]
        [Display(Name = "Unit Cost", Description = "This is retail price.")]
        [CustomValidation(typeof(ProductValidation), "ValidateUnitCost")]
        public double UnitCost
        {
            get { return unitCost; }
            set
            {
                if (value < 0) throw new ArgumentException("Can't be less than 0.");
                ValidationContext context = new ValidationContext(this, null, null);
                context.MemberName = "UnitCost";
                //Validator.ValidateProperty(value, context);

                unitCost = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UnitCost"));
            }
        }

        private string description;
        [DataMember()]
        [Display(Name = "Description", Description = "This is the catalog text for the item.")]
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
        [DataMember()]
        [Display(Name = "Category Name", Description = "The product belongs to the category with this name.")]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string productImagePath;
        [DataMember()]
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

        // Added for DateAdded editing example in DataGridEditing.xaml.
        public DateTime DateAdded { get; set; }

        // Added for CategoryName editing example in DataGridEditing.xaml.
        public string[] CategoryChoices
        {
            get
            {
                return new string[] { "Deception", "Travel", "General", "Communications", "Tools", "Munitions", "Protection"};
            }
        }
    }

    public class ProductValidation
    {
        public static ValidationResult ValidateUnitCost(double value, ValidationContext context)
        {
            string valueString = value.ToString();
            string cents = "";
            int decimalPosition = valueString.IndexOf(".");
            if (decimalPosition != -1)
            {
                cents = valueString.Substring(decimalPosition);
            }
            
            if ((cents != ".75") && (cents != ".99") && (cents != ".95"))
            {
                return new ValidationResult("Retail prices must end with .75, .95, or .99 to be valid.");                
            }
            else
            {
                return ValidationResult.Success;                
            }
 
        }

        public static ValidationResult ValidateProduct(Product product, ValidationContext context)
        {
            if (product.ModelName == product.ModelNumber)
            {
                return new ValidationResult("You can't use the same model number as the model name.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}