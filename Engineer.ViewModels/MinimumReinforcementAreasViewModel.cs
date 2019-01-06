using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using Engineer.Domain;
using Engineer.Services;
using Engineer.Utilities;

namespace Engineer.ViewModels
{
    public class MinimumReinforcementAreasViewModel : ViewModelBase
    {
        #region fields
        private MaterialService concreteService;
        private BarService barService;
        private CrackService crackService;
        private MinimumReinforcementService minReinfService;
        #endregion

        #region properties
        public RelayCommand Calculate { get; private set; }
        public RelayCommand GetTensile { get; private set; }

        private ObservableCollection<Concrete> concreteClasses;
        public ObservableCollection<Concrete> ConcreteClasses
        {
            get
            {
                concreteClasses = new ObservableCollection<Concrete>(concreteService.GetConcreteList());
                return concreteClasses;
            }
        }

        private ObservableCollection<ReinforcingBar> reinforcingBars;
        public ObservableCollection<ReinforcingBar> ReinforcingBars
        {
            get
            {
                reinforcingBars = new ObservableCollection<ReinforcingBar>(barService.GetReinforcingBarList());
                return reinforcingBars;
            }
        }

        private ObservableCollection<Crack> crack;
        public ObservableCollection<Crack> Crack
        {
            get
            {
                crack = new ObservableCollection<Crack>(crackService.GetCrack().OrderByDescending(x => x.Width).ToList());
                return crack;
            }
        }

        private ObservableCollection<MinimumReinforcement> minimumReinforcement;
        public ObservableCollection<MinimumReinforcement> MinimumReinforcement
        {
            get
            {
                return minimumReinforcement;
            }
            private set
            {
                minimumReinforcement = value;
                NotifyPropertyChanged(() => MinimumReinforcement);
            }
        }

        private Concrete selectedConcrete;
        public Concrete SelectedConcrete
        {
            get { return selectedConcrete; }
            private set
            {
                if (value != null)
                {
                    selectedConcrete = value;
                }
            }
        }

        private Crack selectedCrack;
        public Crack SelectedCrack
        {
            get { return selectedCrack; }
            private set
            {
                if (value != null)
                {
                    selectedCrack = value;
                }
            }
        }

        private string sectionWidth;
        public string SectionWidth
        {
            get { return sectionWidth; }
            private set
            {
                sectionWidth = value;
                NotifyPropertyChanged(() => SectionWidth);
            }
        }

        private string sectionHeight;
        public string SectionHeight
        {
            get { return sectionHeight; }
            private set
            {
                sectionHeight = value;
                NotifyPropertyChanged(() => SectionWidth);
            }
        }

        private string cover;
        public string Cover
        {
            get { return cover; }
            private set
            {
                cover = value;
                NotifyPropertyChanged(() => Cover);
            }
        }

        private bool isSlabSelected;
        public bool IsSlabSelected
        {
            get { return isSlabSelected; }
            private set
            {
                isSlabSelected = value;

                if (value == true)
                {
                    sectionWidth = "1000";
                    isWidthEnabled = false;
                }
                else
                {
                    sectionWidth = string.Empty;
                    isWidthEnabled = true;

                }
                NotifyPropertyChanged(() => SectionWidth);
                NotifyPropertyChanged(() => IsWidthEnabled);
            }
        }

        private bool isWidthEnabled = true;
        public bool IsWidthEnabled
        {
            get { return isWidthEnabled; }
            private set
            {
                isWidthEnabled = value;
            }
        }

        private string effectiveTensileStrength;
        public string EffectiveTensileStrength
        {
            get { return effectiveTensileStrength; }
            private set
            {
                effectiveTensileStrength = value;
                NotifyPropertyChanged(() => EffectiveTensileStrength);
            }
        }

        private string stressDistributionCoefficient;
        public string StressDistributionCoefficient
        {
            get { return stressDistributionCoefficient; }
            private set
            {
                stressDistributionCoefficient = value;
                NotifyPropertyChanged(() => StressDistributionCoefficient);
            }
        }

        private string sectionArea;
        public string SectionArea
        {
            get { return sectionArea; }
            private set
            {
                sectionArea = value;
                NotifyPropertyChanged(() => SectionArea);
            }
        }

        private string tensile;
        public string Tensile
        {
            get { return tensile; }
            private set
            {
                tensile = value;
                NotifyPropertyChanged(() => Tensile);
            }
        }

        private string nonUniformStressCoefficient;
        public string NonUniformStressCoefficient
        {
            get { return nonUniformStressCoefficient; }
            private set
            {
                nonUniformStressCoefficient = value;
                NotifyPropertyChanged(() => NonUniformStressCoefficient);
            }
        }
        #endregion

        #region constructor
        public MinimumReinforcementAreasViewModel()
        {
            barService = new BarService();
            concreteService = new MaterialService();
            crackService = new CrackService();
            minReinfService = new MinimumReinforcementService();

            Calculate = new RelayCommand(CalculateExecute);
            GetTensile = new RelayCommand(GetTensileExecute);
        }
        #endregion

        #region methods
        private void GetTensileExecute(object obj)
        {
            tensile = (string) obj;
        }

        private void CalculateExecute(object obj)
        {
            if (IsAllDataComplete())
            {
                double width = Convert.ToDouble(SectionWidth);
                double height = Convert.ToDouble(SectionHeight);
                var section = new Rectangle(width, height);              

                minimumReinforcement = new ObservableCollection<MinimumReinforcement>();

                foreach (var bar in ReinforcingBars.Where(x => x.IsSelected))
                {
                    minimumReinforcement.Add(minReinfService.AddMinimumReinforcement(SelectedConcrete, section, bar, Tensile));
                }

                ShowResults();
            }
        }

        private bool IsAllDataComplete()
        {
            if (SelectedConcrete != null && SelectedCrack != null && Tensile != null && Cover != null && ReinforcingBars.Where(x => x.IsSelected).Any() &&
                !string.IsNullOrEmpty(SectionHeight) && !string.IsNullOrEmpty(SectionWidth))
            {
                return true;
            }
            else
            {             
                MessageBox.Show("Niekompletne dane", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void ShowResults()
        {
            effectiveTensileStrength = minReinfService.GetEffectiveTensileStrenght().ToString("0.00") + " MPa";
            sectionArea = minReinfService.GetSectionArea().ToString("0.000") + " m2";
            stressDistributionCoefficient = minReinfService.GetStressDistributionCoefficient().ToString("0.00");
            nonUniformStressCoefficient = minReinfService.GetNonUniformStressCoefficient().ToString("0.00");

            NotifyPropertyChanged(() => EffectiveTensileStrength);
            NotifyPropertyChanged(() => SectionArea);
            NotifyPropertyChanged(() => StressDistributionCoefficient);
            NotifyPropertyChanged(() => NonUniformStressCoefficient);
            NotifyPropertyChanged(() => MinimumReinforcement);
        }
        #endregion
    }
}
