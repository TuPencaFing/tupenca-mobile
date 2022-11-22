using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_mobile.Model.Dto;
using tupenca_mobile.Services;

namespace tupenca_mobile.ViewModel
{
    [QueryProperty("PencaCompartidaId", "PencaCompartidaId")]
    [QueryProperty("Title", "Title")]
    [QueryProperty("Pozo", "Pozo")]
    [QueryProperty("Costo", "Costo")]

    public partial class PencaCompartidaDetailsViewModel : BaseViewModel
    {
        RestService restService;
        public PencaCompartidaDetailsViewModel(RestService restService) : base(restService)
        {
            this.restService = restService;
            
        }
        [ObservableProperty]
        String myScore;
        [ObservableProperty]
        String myPosition;
        [ObservableProperty]
        String pencaCompartidaId;
        [ObservableProperty]
        String title;
        [ObservableProperty]
        String pozo;
        [ObservableProperty]
        String costo;

        async partial void OnPencaCompartidaIdChanged(string value)
        {
            //pencaCompartida = Newtonsoft.Json.JsonConvert.DeserializeObject<PencaCompartidaDto>(pencaCompartidaDto);
             await getUsuariosByPencaAsync();
             await GetProximosEventosAsync();

        }

        public ObservableCollection<UsuarioScoreExtended> UsuariosScore { get; } = new();

        public ObservableCollection<EventoPrediccionDto> ProximosEventos { get; } = new();

        [RelayCommand]
        async void BackAsync()
        {
            await Shell.Current.GoToAsync("//list");

        }
        [RelayCommand]
        private async Task IngresarResultadoAsync(EventoPrediccionDto eventoPrediccion)
        {
            System.Diagnostics.Debug.WriteLine(" the selected item's name  is:  ");
            restService.ingresarPrediccion(int.Parse(pencaCompartidaId), eventoPrediccion.Id, eventoPrediccion.Prediccion);
            // Here we can remove the seletced obj from the data
        }
        public async Task getUsuariosByPencaAsync()
        {
            try
            {
                var usuariosScore = await restService.getUsersByPenca(int.Parse(pencaCompartidaId));

                UsuariosScore.Clear();

                for(int i=0; i<usuariosScore.Count;i++)
                {
                    var usuarioScoreExtended = new UsuarioScoreExtended() { usuarioScore = usuariosScore[i], Position = i + 1 };
                    if (usuariosScore[i].ID == restService.myId)
                    {
                        myScore = usuariosScore[i].TotalScore.ToString();
                        MyPosition = (i + 1).ToString();
                        usuarioScoreExtended.color = Colors.LightBlue;
                    }
                    UsuariosScore.Add(usuarioScoreExtended);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get usuarios: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            
        }

        public async Task GetProximosEventosAsync()
        {

            try
            {
                var eventos = await restService.getEventosProximos(int.Parse(pencaCompartidaId));
                ProximosEventos.Clear();

                foreach (var evento in eventos)
                {
                    if(evento.Prediccion == null)
                        evento.Prediccion = new PrediccionDto();
                    ProximosEventos.Add(evento);

                }
                    

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get eventos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }

        }
    }
}
