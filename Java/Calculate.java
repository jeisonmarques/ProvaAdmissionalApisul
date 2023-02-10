import java.util.List;
import java.util.Properties;

public class Calculate {

    public static String elevator;
    public List<?> inputList;

    public static float CalculateElevatorUsagePercentual(String elevator, List<?> list) {
        // List<list> list0 = InputRepository();

        int counter = 0;
        int lenght = list.size();

        // for (list iresp : list) {
        //     if (iresp.getElevator().equals(elevator)) {
        //         counter++;
        //     }
        // }
        return (float) counter / lenght * 100;
    }
}
