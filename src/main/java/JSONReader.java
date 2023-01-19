import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

import java.io.File;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.List;

public class JSONReader {
    public static void main(String[] args) {
        JSONParser parser = new JSONParser();
        try {
            File file = new File("input.json");
            String absolutePath = file.getAbsolutePath();
            if (!file.exists() || !file.canRead()) {
                throw new Exception("Cannot read input.json file.");
            }
            FileReader reader = new FileReader(absolutePath);
            Object obj = parser.parse(reader);
            JSONArray jsonArray = (JSONArray) obj;
            List<ElevatorData> elevatorDataList = new ArrayList<ElevatorData>();
            for (Object elevatorData : jsonArray) {
                JSONObject elevator = (JSONObject) elevatorData;
                int floor = (int)((long) elevator.get("andar"));
                String elevatorName = (String) elevator.get("elevador");
                String period = (String) elevator.get("turno");
                ElevatorData eD = new ElevatorData(floor, elevatorName, period);
                elevatorDataList.add(eD);
            }
            ElevatorService elevatorService = new ElevatorService(elevatorDataList);

            List<Integer> andaresMenosUtilizados = elevatorService.andarMenosUtilizado();
            System.out.println("Andar(es) menos usados: " + andaresMenosUtilizados + "\n");

            List<Character> elevadoresMaisFrequentados = elevatorService.elevadorMaisFrequentado();
            System.out.println("Elevador(es) mais frequentados: " + elevadoresMaisFrequentados + "\n");

            List<Character> periodoMaiorFluxoElevadorMaisFrequentado = elevatorService.periodoMaiorFluxoElevadorMaisFrequentado();
            System.out.println("Periodo de maior fluxo (elevador mais frequentado): " + periodoMaiorFluxoElevadorMaisFrequentado + "\n");

            List<Character> elevadorMenosFrequentado = elevatorService.elevadorMenosFrequentado();
            System.out.println("Elevador(es) menos frequentado: " + elevadorMenosFrequentado + "\n");

            List<Character> periodoMenorFluxoElevadorMenosFrequentado = elevatorService.periodoMenorFluxoElevadorMenosFrequentado();
            System.out.println("Periodo de menor fluxo: " + periodoMenorFluxoElevadorMenosFrequentado + "\n");

            List<Character> periodoMaiorUtilizacaoConjuntoElevadores = elevatorService.periodoMaiorUtilizacaoConjuntoElevadores();
            System.out.println("Periodo de maior utilização dos conjuntos de elevadores: " + periodoMaiorUtilizacaoConjuntoElevadores + "\n");

            System.out.printf("Percentual Elevador A: %.2f\n" , elevatorService.percentualDeUsoElevadorA());
            System.out.printf("Percentual Elevador B: %.2f\n" , elevatorService.percentualDeUsoElevadorB());
            System.out.printf("Percentual Elevador C: %.2f\n" , elevatorService.percentualDeUsoElevadorC());
            System.out.printf("Percentual Elevador D: %.2f\n" , elevatorService.percentualDeUsoElevadorD());
            System.out.printf("Percentual Elevador E: %.2f\n" , elevatorService.percentualDeUsoElevadorE());
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}