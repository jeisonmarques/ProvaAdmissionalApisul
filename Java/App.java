public class App {

    public static void main(String[] args) {
        // Dependencies
        ElevatorRepository elevadorRepository = new ElevatorRepository();

        // Inject
        ElevatorService elevadorService = new ElevatorService(elevadorRepository);

        System.out.println(String.join("\t","a. Qual é o andar menos utilizado pelos usuários", elevadorService.andarMenosUtilizado().toString()));

        System.out.println(String.join("\t", "b. Qual é o elevador mais frequentado " + elevadorService.elevadorMaisFrequentado().toString() + ", e o período que se encontra maior fluxo", elevadorService.periodoMaiorFluxoElevadorMaisFrequentado().toString()));

        System.out.println(String.join("\t", "c. Qual é o elevador menos frequentado " + elevadorService.elevadorMenosFrequentado().toString() + ", e o período que se encontra menor fluxo", elevadorService.periodoMenorFluxoElevadorMenosFrequentado().toString()));

        System.out.println(String.join("\t","d. Qual o período de maior utilização do conjunto de elevadores", elevadorService.periodoMaiorUtilizacaoConjuntoElevadores().toString()));

        System.out.println("e. Qual o percentual de uso de cada elevador com relação a todos os serviços prestados");
        System.out.println("\tO elevador A atingiu o percentual de: " + String.format("%.2f",elevadorService.percentualDeUsoElevadorA()) + "%");
        System.out.println("\tO elevador B atingiu o percentual de: " + String.format("%.2f",elevadorService.percentualDeUsoElevadorB()) + "%");
        System.out.println("\tO elevador C atingiu o percentual de: " + String.format("%.2f",elevadorService.percentualDeUsoElevadorC()) + "%");
        System.out.println("\tO elevador D atingiu o percentual de: " + String.format("%.2f",elevadorService.percentualDeUsoElevadorD()) + "%");
        System.out.println("\tO elevador E atingiu o percentual de: " + String.format("%.2f",elevadorService.percentualDeUsoElevadorE()) + "%");

    }
}