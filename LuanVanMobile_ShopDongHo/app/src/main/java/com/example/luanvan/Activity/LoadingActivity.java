package com.example.luanvan.Activity;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.SystemClock;
import android.widget.ProgressBar;

import androidx.appcompat.app.AppCompatActivity;

import com.example.luanvan.R;

public class LoadingActivity extends AppCompatActivity {

    ProgressBar progressBar;
    Handler handler = new Handler();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_loading);

        progressBar = findViewById(R.id.progressBar);
        progressBar.getProgressDrawable().setColorFilter(
                Color.BLACK, android.graphics.PorterDuff.Mode.SRC_IN);

        doStartProgressBar1();
       /* new Handler().postDelayed(() ->
        {
            Intent intent = new Intent(LoadingActivity.this, LoginActivity.class);
            startActivity(intent);
            finish();
        }, 5000);*/
        getSupportActionBar().hide();

    }

    private void doStartProgressBar1() {
        final int MAX = 110;
        this.progressBar.setMax(MAX);

        Thread thread = new Thread(new Runnable() {

            @Override
            public void run() {
                handler.post(new Runnable() {
                    public void run() {

                    }
                });
                for (int i = 0; i < MAX; i++) {
                    final int progress = i + 1;
                    // Do something (Download, Upload, Update database,..)
                    SystemClock.sleep(20); // Sleep 20 milliseconds.

                    // Update interface.
                    handler.post(new Runnable() {
                        public void run() {
                            progressBar.setProgress(progress);
                            int percent = (progress * 100) / MAX;

                            if (progress == MAX) {
                                new Handler().postDelayed(() ->
                                {
                                    Intent intent = new Intent(LoadingActivity.this, LoginActivity.class);
                                    startActivity(intent);
                                    finish();
                                }, 500);
                            }
                        }
                    });
                }
            }
        });
        thread.start();
    }
}