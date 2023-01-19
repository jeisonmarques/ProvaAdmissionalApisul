import java.util.*;
import java.util.stream.Collectors;

public class ElevatorService implements IElevatorService{
    private List<ElevatorData> elevatorDataList;

    public ElevatorService(List<ElevatorData> elevatorDataList) {
        this.elevatorDataList = elevatorDataList;
    }

    @Override
    public List<Integer> andarMenosUtilizado() {
        Map<Integer, Integer> andarContagem = new HashMap<Integer, Integer>();

        for (ElevatorData data : elevatorDataList) {
            int andar = data.getFloor();
            if (andarContagem.containsKey(andar)) {
                andarContagem.put(andar, andarContagem.get(andar) + 1);
            } else {
                andarContagem.put(andar, 1);
            }
        }

        int menorContagem = Integer.MAX_VALUE;
        for (int contagem : andarContagem.values()) {
            if (contagem < menorContagem) {
                menorContagem = contagem;
            }
        }

        List<Integer> andaresMenosUtilizados = new ArrayList<Integer>();
        for (Map.Entry<Integer, Integer> entry : andarContagem.entrySet()) {
            if (entry.getValue() == menorContagem) {
                andaresMenosUtilizados.add(entry.getKey());
            }
        }
        return andaresMenosUtilizados;
    }

    @Override
    public List<Character> elevadorMaisFrequentado() {
        Map<Character, Integer> contadorElevador = new HashMap<>();
        for (ElevatorData elevatorData : elevatorDataList) {
            String elevatorNameString = elevatorData.getElevatorName();
            char elevadorNome = elevatorNameString.charAt(0);
            if (contadorElevador.containsKey(elevadorNome)) {
                contadorElevador.put(elevadorNome, contadorElevador.get(elevadorNome) + 1);
            } else {
                contadorElevador.put(elevadorNome, 1);
            }
        }

        int contadorMax = 0;
        char elevadorMaisFreq = ' ';

        for (Map.Entry<Character, Integer> entry : contadorElevador.entrySet()) {
            if (entry.getValue() > contadorMax) {
                contadorMax = entry.getValue();
                elevadorMaisFreq = entry.getKey();
            }
        }

        List<Character> restultado = new ArrayList<>();
        restultado.add(elevadorMaisFreq);
        return restultado;
    }

    @Override
    public List<Character> periodoMaiorFluxoElevadorMaisFrequentado() {
        HashMap<String, HashMap<String, Integer>> periodoElevadorFlux = new HashMap<>();

        for (ElevatorData elevatorData : elevatorDataList) {
            String elevador = elevatorData.getElevatorName();
            String periodo = elevatorData.getPeriod();

            if (!periodoElevadorFlux.containsKey(elevador)) {
                periodoElevadorFlux.put(elevador, new HashMap<String, Integer>());
            }
            HashMap<String, Integer> periodoCont = periodoElevadorFlux.get(elevador);
            if (!periodoCont.containsKey(periodo)) {
                periodoCont.put(periodo, 0);
            }
            periodoCont.put(periodo, periodoCont.get(periodo) + 1);
        }

        String elevadorMaisFreq = "";
        int maxCount = 0;
        for (String elevator : periodoElevadorFlux.keySet()) {
            int count = (int) elevatorDataList.stream().filter(elevatorData -> elevatorData.getElevatorName().equals(elevator)).count();
            if (count > maxCount) {
                maxCount = count;
                elevadorMaisFreq = elevator;
            }
        }

        HashMap<String, Integer> periodCount = periodoElevadorFlux.get(elevadorMaisFreq);
        String periodoMaisFreq = "";
        int periodoContadorMax = 0;
        for (String period : periodCount.keySet()) {
            int count = periodCount.get(period);
            if (count > periodoContadorMax) {
                periodoContadorMax = count;
                periodoMaisFreq = period;
            }
        }

        List<Character> periodoChar = new ArrayList<>();
        for (char c : periodoMaisFreq.toCharArray()) {
            periodoChar.add(c);
        }
        return periodoChar;
    }

    @Override
    public List<Character> elevadorMenosFrequentado() {
        HashMap<String, Integer> contadorElevador = new HashMap<>();

        for (ElevatorData elevatorData : elevatorDataList) {
            String elevador = elevatorData.getElevatorName();
            if (!contadorElevador.containsKey(elevador)) {
                contadorElevador.put(elevador, 0);
            }
            contadorElevador.put(elevador, contadorElevador.get(elevador) + 1);
        }

        String elevadorMenosFreq = "";
        int contadorMin = Integer.MAX_VALUE;
        for (String elevador : contadorElevador.keySet()) {
            int count = contadorElevador.get(elevador);
            if (count < contadorMin) {
                contadorMin = count;
                elevadorMenosFreq = elevador;
            }
        }

        List<Character> elevadorChar = new ArrayList<>();
        for (char c : elevadorMenosFreq.toCharArray()) {
            elevadorChar.add(c);
        }
        return elevadorChar;
    }

    @Override
    public List<Character> periodoMenorFluxoElevadorMenosFrequentado() {
        HashMap<String, HashMap<String, Integer>> periodoElevadorContador = new HashMap<>();

        for (ElevatorData elevatorData : elevatorDataList) {
            String elevador = elevatorData.getElevatorName();
            String periodo = elevatorData.getPeriod();

            if (!periodoElevadorContador.containsKey(elevador)) {
                periodoElevadorContador.put(elevador, new HashMap<String, Integer>());
            }
            if (!periodoElevadorContador.get(elevador).containsKey(periodo)) {
                periodoElevadorContador.get(elevador).put(periodo, 0);
            }
            periodoElevadorContador.get(elevador).put(periodo, periodoElevadorContador.get(elevador).get(periodo) + 1);
        }
        
        String elevadorMenosFreq = "";
        int contadorMin = Integer.MAX_VALUE;
        for (String elevador : periodoElevadorContador.keySet()) {
            int contador = periodoElevadorContador.get(elevador).values().stream().mapToInt(Integer::intValue).sum();
            if (contador < contadorMin) {
                contadorMin = contador;
                elevadorMenosFreq = elevador;
            }
        }

        String periodoMenosFreq = "";
        contadorMin = Integer.MAX_VALUE;
        for (String period : periodoElevadorContador.get(elevadorMenosFreq).keySet()) {
            int contador = periodoElevadorContador.get(elevadorMenosFreq).get(period);
            if (contador < contadorMin) {
                contadorMin = contador;
                periodoMenosFreq = period;
            }
        }
        return periodoMenosFreq.chars().mapToObj(e -> (char) e).collect(Collectors.toList());
    }

    @Override
    public List<Character> periodoMaiorUtilizacaoConjuntoElevadores() {
        Map<Character, Integer> periodos = new HashMap<>();

        periodos.put('M', 0);
        periodos.put('V', 0);
        periodos.put('N', 0);

        for (ElevatorData elevatorData : elevatorDataList) {
            String periodoStringNome = elevatorData.getPeriod();
            char periodo = periodoStringNome.charAt(0);
            periodos.put(periodo, periodos.get(periodo) + 1);
        }

        List<Character> periodoMaiorUtilizacao = new ArrayList<>();
        int valorMax = 0;
        for (Map.Entry<Character, Integer> entry : periodos.entrySet()) {
            if (entry.getValue() > valorMax) {
                periodoMaiorUtilizacao.clear();
                periodoMaiorUtilizacao.add(entry.getKey());
                valorMax = entry.getValue();
            } else if (entry.getValue() == valorMax) {
                periodoMaiorUtilizacao.add(entry.getKey());
            }
        }
        return periodoMaiorUtilizacao;
    }

    @Override
    public float percentualDeUsoElevadorA() {
        int totalDeUsoElevadorA = 0;
        int totalDeUsoElevadores = elevatorDataList.size();
        for (ElevatorData elevatorData : elevatorDataList) {
            if (elevatorData.getElevatorName().equals("A")) {
                totalDeUsoElevadorA++;
            }
        }
        return (float) totalDeUsoElevadorA / totalDeUsoElevadores * 100;
    }

    @Override
    public float percentualDeUsoElevadorB() {
        int totalDeUsoElevadorB = 0;
        int totalDeUsoElevadores = elevatorDataList.size();
        for (ElevatorData elevatorData : elevatorDataList) {
            if (elevatorData.getElevatorName().equals("B")) {
                totalDeUsoElevadorB++;
            }
        }
        return (float) totalDeUsoElevadorB / totalDeUsoElevadores * 100;
    }

    @Override
    public float percentualDeUsoElevadorC() {
        int totalDeUsoElevadorC = 0;
        int totalDeUsoElevadores = elevatorDataList.size();
        for (ElevatorData elevatorData : elevatorDataList) {
            if (elevatorData.getElevatorName().equals("C")) {
                totalDeUsoElevadorC++;
            }
        }
        return (float) totalDeUsoElevadorC / totalDeUsoElevadores * 100;
    }

    @Override
    public float percentualDeUsoElevadorD() {
        int totalDeUsoElevadorD = 0;
        int totalDeUsoElevadores = elevatorDataList.size();
        for (ElevatorData elevatorData : elevatorDataList) {
            if (elevatorData.getElevatorName().equals("D")) {
                totalDeUsoElevadorD++;
            }
        }
        return (float) totalDeUsoElevadorD / totalDeUsoElevadores * 100;
    }

    @Override
    public float percentualDeUsoElevadorE() {
        int totalDeUsoElevadorE = 0;
        int totalDeUsoElevadores = elevatorDataList.size();
        for (ElevatorData elevatorData : elevatorDataList) {
            if (elevatorData.getElevatorName().equals("E")) {
                totalDeUsoElevadorE++;
            }
        }
        return (float) totalDeUsoElevadorE / totalDeUsoElevadores * 100;
    }
}
