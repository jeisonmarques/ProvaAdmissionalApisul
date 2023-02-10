import java.io.File;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;

public class ElevatorRepository implements IElevatorRepository {

    @Override
    public String StringJsonStream() {
        String json;
        try {
            File file = new File("input.json");
            String absolutePath = file.getAbsolutePath();

            json = String.join(" ",
                    Files.readAllLines(
                            Paths.get(absolutePath),
                            StandardCharsets.UTF_8));

            return json;
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }
}
