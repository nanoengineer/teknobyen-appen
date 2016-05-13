package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Base64;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.jsoup.Jsoup;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.ExecutionException;

public class MachineStatus extends AppCompatActivity {
    //This class is for opening the web browser inside the app.

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);


        setContentView(R.layout.activity_washing_machine_status);

        try {
            urlScraping();
        } catch (IOException e) {
            System.out.println("MachineStatus IOException");
            e.printStackTrace();
        } catch (InterruptedException e) {
            System.out.println("MachineStatus InterruptedException");
            e.printStackTrace();
        } catch (ExecutionException e) {
            System.out.println("MachineStatus ExecutionException");
            e.printStackTrace();
        }


    }

    private void urlScraping() throws IOException, ExecutionException, InterruptedException {


        SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
        String name = sharedPref.getString("username", "");
        String password = sharedPref.getString("password", "");

        Retrievedata retrieveStatus = new Retrievedata();
        String machineStatusString = retrieveStatus.execute(name, password).get();

        int[] linearLayoutIds = {R.id.machine0, R.id.machine1, R.id.machine2, R.id.machine3, R.id.machine4, R.id.machine5};
        int[] textViewIds = {R.id.statusText0, R.id.statusText1, R.id.statusText2, R.id.statusText3, R.id.statusText4, R.id.statusText5};


        if (machineStatusString != null) {
            String[] statusArray = machineStatusString.split(",");
            for (int i = 0; i < statusArray.length; i++) {
                TextView text = (TextView) findViewById(textViewIds[i]);
                LinearLayout layout = (LinearLayout) findViewById((linearLayoutIds[i]));
                text.setText(statusArray[i]);
                if (layout != null) {
                    if (statusArray[i].equals("Ledig")) {
                        layout.setBackgroundColor(Color.parseColor("#AAFFAA"));
                    } else {
                        layout.setBackgroundColor(Color.parseColor("#ffa5a5"));
                    }

                }
            }

        } else {
            Toast.makeText(MachineStatus.this, "Could not load status.", Toast.LENGTH_LONG).show();
        }

    }

}

class Retrievedata extends AsyncTask<String, Void, String> {
    @Override
    protected String doInBackground(String... params) {
        try {
            URL statusUrl = new URL("http://129.241.152.11/LaundryState?lg=2&ly=9131");
        } catch (MalformedURLException e) {
            System.out.println("MachineStatus MalformedURLException");
            e.printStackTrace();
        }

        String userPassword = params[0] + ":" + params[1];
        String encoding = Base64.encodeToString(userPassword.getBytes(), Base64.DEFAULT);

        org.jsoup.nodes.Document document = null;
        try {
            document = Jsoup
                    .connect("http://129.241.152.11/LaundryState?lg=2&ly=9131")
                    .header("Authorization", "Basic " + encoding)
                    .get();

            Elements data = document.getElementsByClass("p");

            String status = "";
            for (int i = 0; i < data.size(); i++) {
                status += data.get(i).toString().split("<br>")[1] + ",";
            }
            return status;

        } catch (IOException e) {
            System.out.println("MachineStatus IOException");
            e.printStackTrace();
            return null;
        }
    }
}

