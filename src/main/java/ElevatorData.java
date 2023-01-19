import java.util.ArrayList;
import java.util.List;

public class ElevatorData {
    private int floor;
    private String elevatorName;
    private String period;

    private List<ElevatorData> elevatorDataList;
    public ElevatorData(int floor, String elevatorName, String period) {
        this.elevatorDataList = new ArrayList<ElevatorData>();
        this.floor = floor;
        this.elevatorName = elevatorName;
        this.period = period;
    }

    public int getFloor() {
        return floor;
    }

    public void setFloor(int floor) {
        this.floor = floor;
    }

    public String getElevatorName() {
        return elevatorName;
    }

    public void setElevatorName(String elevatorName) {
        this.elevatorName = elevatorName;
    }

    public String getPeriod() {
        return period;
    }

    public void setPeriod(String period) {
        this.period = period;
    }

}
