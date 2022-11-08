using CommunityToolkit.Mvvm.ComponentModel;
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
    [QueryProperty(nameof(PencaCompartidaDto), "PencaCompartidaDto")]
    public partial class PencaCompartidaDetailsViewModel : BaseViewModel
    {
        RestService restService;
        public PencaCompartidaDetailsViewModel(RestService restService) : base(restService)
        {
            this.restService = restService;
            
        }

        [ObservableProperty]
        PencaCompartidaDto pencaCompartidaDto;

        public ObservableCollection<UsuarioScoreDTO> UsuariosScore { get; } = new();


        public async Task getUsuariosByPencaAsync()
        {
            try
            {
                var usuariosScore = await restService.getUsersByPenca(pencaCompartidaDto.Id);

                if (usuariosScore.Count != 0)
                    UsuariosScore.Clear();

                foreach (var usuario in usuariosScore)
                    UsuariosScore.Add(usuario);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get usuarios: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            
        }
    }
}
