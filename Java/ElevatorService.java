import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

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

        var repo = _repository.StringJsonStream();

        InputResponse[] response = new Gson().fromJson(repo, InputResponse[].class);
        List<InputResponse> list = new ArrayList<>(Arrays.asList(response));
        
        HashMap<String, Integer> elevatorCount = new HashMap<>();

        for (InputResponse iresp : list) {
            String elevator = iresp.getElevator();
            if (!elevatorCount.containsKey(elevator)) {
                elevatorCount.put(elevator, 0);
            }
            elevatorCount.put(elevator, elevatorCount.get(elevator) + 1);
        }

        String elevatorLessUsed = "";
        int minimusCount = Integer.MAX_VALUE;
        for (String elevator : elevatorCount.keySet()) {
            int count = elevatorCount.get(elevator);
            if (count < minimusCount) {
                minimusCount = count;
                elevatorLessUsed = elevator;
            }
        }

        List<Character> elevatorChar = new ArrayList<>();
        for (char c : elevatorLessUsed.toCharArray()) {
            elevatorChar.add(c);
        }
        return elevatorChar;
    }

    @Override
    public List<Character> periodoMenorFluxoElevadorMenosFrequentado() {

        var repo = _repository.StringJsonStream();

        InputResponse[] response = new Gson().fromJson(repo, InputResponse[].class);
        List<InputResponse> list = new ArrayList<>(Arrays.asList(response));
        
        HashMap<String, HashMap<String, Integer>> shiftCount = new HashMap<>();

        for (InputResponse iresp : list) {
            String elevator = iresp.getElevator();
            String shift = iresp.getShift();

            if (!shiftCount.containsKey(elevator)) {
                shiftCount.put(elevator, new HashMap<String, Integer>());
            }
            if (!shiftCount.get(elevator).containsKey(shift)) {
                shiftCount.get(elevator).put(shift, 0);
            }
            shiftCount.get(elevator).put(shift, shiftCount.get(elevator).get(shift) + 1);
        }
        
        String elevatorLessUsed = "";
        int minimusCount = Integer.MAX_VALUE;
        for (String elevator : shiftCount.keySet()) {
            int counter = shiftCount.get(elevator).values().stream().mapToInt(Integer::intValue).sum();
            if (counter < minimusCount) {
                minimusCount = counter;
                elevatorLessUsed = elevator;
            }
        }

        String shiftLessUsed = "";
        minimusCount = Integer.MAX_VALUE;
        for (String period : shiftCount.get(elevatorLessUsed).keySet()) {
            int counter = shiftCount.get(elevatorLessUsed).get(period);
            if (counter < minimusCount) {
                minimusCount = counter;
                shiftLessUsed = period;
            }
        }
        return shiftLessUsed.chars().mapToObj(e -> (char) e).collect(Collectors.toList());
    }

    @Override
    public List<Character> periodoMaiorUtilizacaoConjuntoElevadores() {

        var repo = _repository.StringJsonStream();

        InputResponse[] response = new Gson().fromJson(repo, InputResponse[].class);
        List<InputResponse> list = new ArrayList<>(Arrays.asList(response));
        
        Map<Character, Integer> shift = new HashMap<>();

        shift.put('M', 0);
        shift.put('V', 0);
        shift.put('N', 0);

        for (InputResponse iresp : list) {
            String shiftS = iresp.getShift();
            char shif = shiftS.charAt(0);
            shift.put(shif, shift.get(shif) + 1);
        }

        List<Character> shiftMostUsed = new ArrayList<>();
        int maxValues = 0;
        for (Map.Entry<Character, Integer> entry : shift.entrySet()) {
            if (entry.getValue() > maxValues) {
                shiftMostUsed.clear();
                shiftMostUsed.add(entry.getKey());
                maxValues = entry.getValue();
            } else if (entry.getValue() == maxValues) {
                shiftMostUsed.add(entry.getKey());
            }
        }
        return shiftMostUsed;
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
        // int totalDeUsoElevadorE = 0;
        // int totalDeUsoElevadores = pesquisaInputResponse.size();
        // for (InputResponse iresp : pesquisaInputResponse) {
        //     if (iresp.getElevator().equals("E")) {
        //         totalDeUsoElevadorE++;
        //     }
        // }
        // return (float) totalDeUsoElevadorE / totalDeUsoElevadores * 100;
        return 0;
    }

}
