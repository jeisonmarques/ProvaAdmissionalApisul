import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.google.gson.Gson;

public class ElevatorService implements IElevatorService {
    private ElevatorRepository _repository;

    public List<InputResponse> pesquisaInputResponse;
    public InputResponse elevadorVerificar;

    @Inject
    public ElevatorService(ElevatorRepository repository) {
        _repository = repository;
    }

    @Override
    public List<Integer> andarMenosUtilizado() {
        
        var repo = _repository.StringJsonStream();

        InputResponse[] response = new Gson().fromJson(repo, InputResponse[].class);
        List<InputResponse> list = new ArrayList<>(Arrays.asList(response));

        Map<Integer, Integer> floorCount = new HashMap<Integer, Integer>();

        for (InputResponse iresp : list) {
            int level = iresp.getApartmentFloor();
            if (floorCount.containsKey(level)) {
                floorCount.put(level, floorCount.get(level) + 1);
            } else {
                floorCount.put(level, 1);
            }
        }

        int minimusCount = Integer.MAX_VALUE;
        for (int countage : floorCount.values()) {
            if (countage < minimusCount) {
                minimusCount = countage;
            }
        }

        List<Integer> apartmentFloorLessUsed = new ArrayList<Integer>();
        for (Map.Entry<Integer, Integer> entry : floorCount.entrySet()) {
            if (entry.getValue() == minimusCount) {
                apartmentFloorLessUsed.add(entry.getKey());
            }
        }
        return apartmentFloorLessUsed;
    }

    @Override
    public List<Character> elevadorMaisFrequentado() {

        var repo = _repository.StringJsonStream();

        InputResponse[] response = new Gson().fromJson(repo, InputResponse[].class);
        List<InputResponse> list = new ArrayList<>(Arrays.asList(response));
      
        Map<Character, Integer> elevatorCount = new HashMap<>();
        for (InputResponse iresp : list) {
            String elevator = iresp.getElevator();
            char elevatorChar = elevator.charAt(0);
            if (elevatorCount.containsKey(elevatorChar)) {
                elevatorCount.put(elevatorChar, elevatorCount.get(elevatorChar) + 1);
            } else {
                elevatorCount.put(elevatorChar, 1);
            }
        }

        int maxCount = 0;
        char elevatorMoreUsed = ' ';

        for (Map.Entry<Character, Integer> entry : elevatorCount.entrySet()) {
            if (entry.getValue() > maxCount) {
                maxCount = entry.getValue();
                elevatorMoreUsed = entry.getKey();
            }
        }

        List<Character> result = new ArrayList<>();
        result.add(elevatorMoreUsed);
        return result;

    }

    @Override
    public List<Character> periodoMaiorFluxoElevadorMaisFrequentado() {
        var repo = _repository.StringJsonStream();

        InputResponse[] response = new Gson().fromJson(repo, InputResponse[].class);
        List<InputResponse> list = new ArrayList<>(Arrays.asList(response));
      
        HashMap<String, HashMap<String, Integer>> elevatorShiftFlow = new HashMap<>();

        for (InputResponse resp : list) {
            String elevator = resp.getElevator();
            String shft = resp.getShift();

            if (!elevatorShiftFlow.containsKey(elevator)) {
                elevatorShiftFlow.put(elevator, new HashMap<String, Integer>());
            }

            HashMap<String, Integer> shftCont = elevatorShiftFlow.get(elevator);
            if (!shftCont.containsKey(shft)) {
                shftCont.put(shft, 0);
            }
            shftCont.put(shft, shftCont.get(shft) + 1);
        }

        String elevatorMaisFreq = "";
        int maxCount = 0;
        for (String elevator : elevatorShiftFlow.keySet()) {
            int count = (int) list.stream().filter(resp -> resp.getElevator().equals(elevator)).count();
            if (count > maxCount) {
                maxCount = count;
                elevatorMaisFreq = elevator;
            }
        }

        HashMap<String, Integer> periodCount = elevatorShiftFlow.get(elevatorMaisFreq);
        String shftMaisFreq = "";
        int shftMaximus = 0;
        for (String period : periodCount.keySet()) {
            int count = periodCount.get(period);
            if (count > shftMaximus) {
                shftMaximus = count;
                shftMaisFreq = period;
            }
        }

        List<Character> shftChar = new ArrayList<>();
        for (char c : shftMaisFreq.toCharArray()) {
            shftChar.add(c);
        }
        return shftChar;
    }

    @Override
    public List<Character> elevadorMenosFrequentado() {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public List<Character> periodoMenorFluxoElevadorMenosFrequentado() {
        
        return null;
    }

    @Override
    public List<Character> periodoMaiorUtilizacaoConjuntoElevadores() {
        
        return null;
    }

    @Override
    public float percentualDeUsoElevadorA() {
       
        return 0;
    }

    @Override
    public float percentualDeUsoElevadorB() {
        // return (float)(((pesquisaInput.Count(x => x.elevador == 'B')) * 100.0) /
        // (pesquisaInput.Count) / 100.0);
        return 0;
    }

    @Override
    public float percentualDeUsoElevadorC() {
        // return (float)(((pesquisaInput.Count(x => x.elevador == 'C')) * 100.0) /
        // (pesquisaInput.Count) / 100.0);
        return 0;
    }

    @Override
    public float percentualDeUsoElevadorD() {
        // return (float)(((pesquisaInput.Count(x => x.elevador == 'D')) * 100.0) /
        // (pesquisaInput.Count) / 100.0);
        return 0;
    }

    @Override
    public float percentualDeUsoElevadorE() {
        // return (float)(((pesquisaInput.Count(x => x.elevador == 'E')) * 100.0) /
        // (pesquisaInput.Count) / 100.0);
        return 0;
    }

}
