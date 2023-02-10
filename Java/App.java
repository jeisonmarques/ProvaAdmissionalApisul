public class App {

    public static void main(String[] args) {
        // Dependencies
        ElevatorRepository elevadorRepository = new ElevatorRepository();

        // Inject
        ElevatorService elevadorService = new ElevatorService(elevadorRepository);

        System.out.println(String.join("\t","a. Qual é o andar menos utilizado pelos usuários", elevadorService.andarMenosUtilizado().toString()));

        System.out.println(String.join("\t","b. Qual é o elevador mais frequentado " + elevadorService.elevadorMaisFrequentado().toString() + ", e o período que se encontra maior fluxo", elevadorService.periodoMaiorFluxoElevadorMaisFrequentado().toString()));


    }
}