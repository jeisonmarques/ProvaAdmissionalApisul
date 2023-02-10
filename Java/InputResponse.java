import java.util.ArrayList;
import java.util.List;

public class InputResponse {
    private int andar;
    private String elevador;
    private String turno;

    private List<InputResponse> responseCount; //count the amount of response

    public InputResponse(int andar, String elevador, String turno) {
        this.andar = andar;
        this.elevador = elevador;
        this.turno = turno;
        this.responseCount = new ArrayList<InputResponse>();
    }

    public int getApartmentFloor() {
        return andar;
    }

    public void setApartmentFloor(int andar) {
        this.andar = andar;
    }

    public String getElevator() {
        return elevador;
    }

    public void setElevator(String elevador) {
        this.elevador = elevador;
    }

    public String getShift() {
        return turno;
    }

    public void setShift(String turno) {
        this.turno = turno;
    }
}
