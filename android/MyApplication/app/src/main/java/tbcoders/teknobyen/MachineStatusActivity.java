package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import java.util.concurrent.ExecutionException;

import tbcoders.teknobyen.urlconnections.RetreiveWashingMachineStatus;

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
                if (refreshData()) {
                    swipeLayout.setRefreshing(false);
                }
            }
        });
    }

    private boolean refreshData() {
        try {
            urlScraping();
        } catch (InterruptedException e) {
            System.out.println("MachineStatusActivity InterruptedException");
            e.printStackTrace();
        } catch (ExecutionException e) {
            System.out.println("MachineStatusActivity ExecutionException");
            e.printStackTrace();
        }

        return true;
    }


    private void urlScraping() throws ExecutionException, InterruptedException {

        RetreiveWashingMachineStatus retrieveStatus = new RetreiveWashingMachineStatus();
        SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
        String name = sharedPref.getString("username", "");
        String password = sharedPref.getString("password", "");
        String machineStatusString = retrieveStatus.execute(name, password).get();

        int[] linearLayoutIds = {R.id.machine0, R.id.machine1, R.id.machine2, R.id.machine3, R.id.machine4, R.id.machine5};
        int[] textViewIds = {R.id.statusText0, R.id.statusText1, R.id.statusText2, R.id.statusText3, R.id.statusText4, R.id.statusText5};


        if (machineStatusString != null) {
            String[] statusArray = machineStatusString.split(",");
            for (int i = 0; i < statusArray.length; i++) {
                TextView text = (TextView) findViewById(textViewIds[i]);
                LinearLayout layout = (LinearLayout) findViewById((linearLayoutIds[i]));
                text.setText(statusArray[i]);
                if (statusArray[i].substring(0,5).equals("Ledig")) {
                    layout.setBackgroundResource(R.color.washingmachineFree);
                } else {
                    layout.setBackgroundResource(R.color.washingmachineBusy);
                }


            }

        } else {
            for (int i : linearLayoutIds) {
                LinearLayout layout = (LinearLayout) findViewById(i);
                layout.setBackgroundResource(R.color.washingmachineUnavailable);
            }
            for (int i : textViewIds) {
                TextView text = (TextView) findViewById(i);
                text.setText(R.string.ingenstatus);
            }
            Toast.makeText(MachineStatusActivity.this, "Kunne ikke koble til maskiner", Toast.LENGTH_LONG).show();
        }
    }

}

