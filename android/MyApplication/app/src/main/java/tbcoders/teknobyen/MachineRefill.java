package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.webkit.WebView;

public class MachineRefill extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_webview);
        //Oppretter nettleser og henter inn brukernavn og passord fra telefonen
        WebView webView = (WebView)findViewById(R.id.webView);
        webView.setWebViewClient(new MyWebViewClient());
        SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
        String name = sharedPref.getString("username", "");
        String password = Base64EncryptDecrypt.decrypt(sharedPref.getString("password", ""));
        webView.setHttpAuthUsernamePassword("http://129.241.152.11/", "", name, password);

        //Setup for webView
        webView.getSettings().setJavaScriptEnabled(true);
        webView.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        webView.getSettings().setDomStorageEnabled(true);
        webView.setVerticalScrollBarEnabled(false);
        webView.getSettings().setSupportMultipleWindows(true);
        webView.getSettings().setBuiltInZoomControls(true);
        webView.getSettings().setDisplayZoomControls(false);
        webView.setInitialScale(130);
        webView.loadUrl("http://129.241.152.11/SaldoForm?lg=2&ly=9131");
    }
}
