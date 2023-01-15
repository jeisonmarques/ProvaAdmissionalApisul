using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Interfaces;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Repositories.Interfaces;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador
{
	public sealed class HsElevadorApp : IHsElevadorApp
	{
		private readonly IHsElevadorRepository _hsElevadorRepository;

		public HsElevadorApp(IHsElevadorRepository hsElevadorRepository)
		{
			_hsElevadorRepository = hsElevadorRepository;
		}

		public void Upload(List<IFormFile> files)
		{
			long size = files.Sum(f => f.Length);

			foreach (var file in files)
			{
				if (file.Length > 0)
				{
					using (Stream stream = file.OpenReadStream())
					{
						using (StreamReader sr = new(stream))
						{
							string conteudoArquivo = sr.ReadToEnd();
							IEnumerable<HsElevadorEntity> hsElevadoresEntity = JsonConvert.DeserializeObject<IEnumerable<HsElevadorEntity>>(conteudoArquivo);
							_hsElevadorRepository.Incluir(hsElevadoresEntity);
						}
					}
				}
			}
		}

		public void Excluir()
		{
			_hsElevadorRepository.Excluir();
		}
	}
}