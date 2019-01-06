using System;
using System.Collections.ObjectModel;
using Engineer.Domain;
using Engineer.Services;

namespace Engineer.ViewModels
{
    public class ConcreteDesignPropertiesViewModel
    {        
        public string WindowTitle { get; private set; }
        private MaterialService concreteService;

        private ObservableCollection<Concrete> concretesDesignProperties;
        public ObservableCollection<Concrete> ConcretesDesignProperties
        {
            get
            {
                concretesDesignProperties = new ObservableCollection<Concrete>(concreteService.GetConcreteList());
                return concretesDesignProperties;
            }
        }       

        public ConcreteDesignPropertiesViewModel()
        {
            WindowTitle = "Klasy betonów wg PN-EN-1992-1-1";
            concreteService = new MaterialService();            
        }                    
    }
}