public class App {

    public static void main(String[] args) {
        // Dependencies
        ElevatorRepository elevadorRepository = new ElevatorRepository();

        // Inject
        ElevatorService elevadorService = new ElevatorService(elevadorRepository);

        System.out.println(String.join("\t","a. Qual é o andar menos utilizado pelos usuários", elevadorService.andarMenosUtilizado().toString()));

        System.out.println(String.join("\t","b. Qual é o elevador mais frequentado " + elevadorService.elevadorMaisFrequentado().toString() + ", e o período que se encontra maior fluxo", elevadorService.periodoMaiorFluxoElevadorMaisFrequentado().toString()));

        System.out.println(String.join("\t","c. Qual é o elevador menos frequentado " + elevadorService.elevadorMenosFrequentado().toString() + ", e o período que se encontra menor fluxo", elevadorService.periodoMenorFluxoElevadorMenosFrequentado().toString()));

        System.out.println(String.join("\t","d. Qual o período de maior utilização do conjunto de elevadores", elevadorService.periodoMaiorUtilizacaoConjuntoElevadores().toString()));

        System.out.println("e. Qual o percentual de uso de cada elevador com relação a todos os serviços prestados");
        System.out.println("O elevador A atingiu o percentual de: " + elevadorService.percentualDeUsoElevadorA() + "%");
        System.out.println("O elevador B atingiu o percentual de: " + elevadorService.percentualDeUsoElevadorB() + "%");
        System.out.println("O elevador C atingiu o percentual de: " + elevadorService.percentualDeUsoElevadorC() + "%");
        System.out.println("O elevador D atingiu o percentual de: " + elevadorService.percentualDeUsoElevadorD() + "%");
        System.out.println("O elevador E atingiu o percentual de: " + elevadorService.percentualDeUsoElevadorE() + "%");

    }
}