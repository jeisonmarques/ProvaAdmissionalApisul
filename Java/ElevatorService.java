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
      
        return null;
    }

    @Override
    public List<Character> periodoMaiorFluxoElevadorMaisFrequentado() {
        // TODO Auto-generated method stub
        return null;
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
