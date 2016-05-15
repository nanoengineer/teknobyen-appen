package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.widget.SwipeRefreshLayout;
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

public class MachineStatusActivity extends AppCompatActivity {
    //This class is for opening the web browser inside the app.
    SwipeRefreshLayout swipeLayout;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_washing_machine_status);

        refreshData();

        swipeLayout = (SwipeRefreshLayout) findViewById(R.id.machineStatusSwipe);
        swipeLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                System.out.println("Refreshing");
                if(refreshData()){
                    swipeLayout.setRefreshing(false);
                }
            }
        });
    }

    private boolean refreshData() {
        try {
            urlScraping();
        } catch (IOException e) {
            System.out.println("MachineStatusActivity IOException");
            e.printStackTrace();
        } catch (InterruptedException e) {
            System.out.println("MachineStatusActivity InterruptedException");
            e.printStackTrace();
        } catch (ExecutionException e) {
            System.out.println("MachineStatusActivity ExecutionException");
            e.printStackTrace();
        }

        return true;
    }


    private void urlScraping() throws IOException, ExecutionException, InterruptedException {


        SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
        String name = sharedPref.getString("username", "");
        String password = sharedPref.getString("password", "");

        RetreiveWashingMachineStatus retrieveStatus = new RetreiveWashingMachineStatus();
        String machineStatusString = retrieveStatus.execute(name, password).get();

        int[] linearLayoutIds = {R.id.machine0, R.id.machine1, R.id.machine2, R.id.machine3, R.id.machine4, R.id.machine5};
        int[] textViewIds = {R.id.statusText0, R.id.statusText1, R.id.statusText2, R.id.statusText3, R.id.statusText4, R.id.statusText5};


        if (machineStatusString != null) {
            String[] statusArray = machineStatusString.split(",");
            for (int i = 0; i < statusArray.length; i++) {
                TextView text = (TextView) findViewById(textViewIds[i]);
                LinearLayout layout = (LinearLayout) findViewById((linearLayoutIds[i]));
                text.setText(statusArray[i]);
                    if (statusArray[i].equals("Ledig")) {
                        layout.setBackgroundColor(Color.parseColor("#AAFFAA"));
                    } else {
                        layout.setBackgroundColor(Color.parseColor("#ffa5a5"));
                    }


            }

        } else {
            for (int i : linearLayoutIds) {
                LinearLayout layout = (LinearLayout)findViewById(i);
                layout.setBackgroundColor(Color.parseColor("#f5c8f9"));
            }
            for (int i : textViewIds){
                TextView text = (TextView)findViewById(i);
                text.setText(R.string.ingenstatus);
            }
            Toast.makeText(MachineStatusActivity.this, "Kunne ikke koble til maskiner", Toast.LENGTH_LONG).show();
        }
    }

}

