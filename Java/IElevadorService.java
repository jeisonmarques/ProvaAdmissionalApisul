import java.util.List;

public interface IElevadorService {
	
	/** Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). */
	List<Integer> andarMenosUtilizado();
	
	/** Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). */
	List<Character> elevadorMaisFrequentado();
	
	/** Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). */
	List<Character> periodoMaiorFluxoElevadorMaisFrequentado();
	
	/** Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). */
	List<Character> elevadorMenosFrequentado();
	
	/** Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). */
	List<Character> periodoMenorFluxoElevadorMenosFrequentado();
	
	/** Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. */
	List<Character> periodoMaiorUtilizacaoConjuntoElevadores();
	
	/** Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. */
	float percentualDeUsoElevadorA();
	
	/** Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. */
	float percentualDeUsoElevadorB();
	
	/** Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. */
	float percentualDeUsoElevadorC();
	
	/** Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. */
	float percentualDeUsoElevadorD();
	
	/** Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. */
	float percentualDeUsoElevadorE();

}