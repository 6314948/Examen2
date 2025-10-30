using HalloWinUI.Data;
using HalloWinUI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HalloWinUI.ViewModels.Pages
{
    public class MainMaisonsViewModel : BaseViewModel
    {
        private readonly IHalloweenDataProvider _dataProvider;
        private MaisonViewModel? _maisonSelectionnee;

        public MaisonViewModel? MaisonSelectionnee
        {
            get => _maisonSelectionnee;
            set
            {
                if (_maisonSelectionnee != value)
                {
                    _maisonSelectionnee = value;
                }
            }
        }

        public ObservableCollection<MaisonViewModel> Maisons { get; }

        private string _nouvelleMaison = string.Empty;
        public string NouvelleMaison {
            get => _nouvelleMaison;
            set
            {
                if (_nouvelleMaison != value)
                {
                    _nouvelleMaison = value;
                }
            }
        }

        public void AjouterMaison()
        {
            var adresse = (NouvelleMaison ?? string.Empty).Trim();
            if (adresse.Length == 0) return;

            var model = new HalloWinUI.Models.Maison(adresse, false);

            Maisons.Add(new MaisonViewModel(model));

            NouvelleMaison = string.Empty;
          
        }


        public MainMaisonsViewModel(IHalloweenDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            Maisons = new ObservableCollection<MaisonViewModel>();
        }

        public void ChargerMaisons()
        {
            Maisons.Clear();
            List<Maison> maisons = _dataProvider.GetMaisons();

            if (maisons != null)
            {
                foreach (Maison maison in maisons)
                {
                    Maisons.Add(new MaisonViewModel(maison));
                }
            }
        }

        public void SupprimerMaison()
        {
            if (MaisonSelectionnee != null)
            {
                Maisons.Remove(MaisonSelectionnee); 
            }
        }
    }
}
