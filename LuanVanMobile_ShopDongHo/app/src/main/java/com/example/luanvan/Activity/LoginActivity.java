package com.example.luanvan.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.luanvan.API.LoginService;
import com.example.luanvan.Model.TaiKhoan;
import com.example.luanvan.R;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {

    EditText edtUserName, edtPassword;
    Button btnLogin;
    List<TaiKhoan> mTaiKhoans;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        edtUserName = findViewById(R.id.edtUserName);
        edtPassword = findViewById(R.id.edtPassword);
        btnLogin = findViewById(R.id.btnLogin);

        mTaiKhoans = new ArrayList<>();
        getListTaiKhoan();

        getSupportActionBar().hide();

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickLogin();
            }
        });
    }

    private void clickLogin() {
        String username = edtUserName.getText().toString().trim();
        String password = edtPassword.getText().toString().trim();
        if (mTaiKhoans == null || mTaiKhoans.isEmpty()) {
            return;
        }
        boolean kq = false;
        for (TaiKhoan tk : mTaiKhoans) {
            String mk = md5(password);
            Log.d("password",mk);
            if (username.equals(tk.getTENDN()) && mk.equals(tk.getMATKHAU()) && tk.getMALOAI().equals("LTK002")) {
                kq = true;
                break;
            }
        }

        if (kq == true) {
            Toast.makeText(LoginActivity.this, "Đăng nhập thành công", Toast.LENGTH_LONG).show();
            new Handler().postDelayed(() ->
            {
                Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                intent.putExtra("tenDN", username);
                startActivity(intent);
                finish();
            }, 2000);
        } else {
            Toast.makeText(LoginActivity.this, "Sai tên đăng nhập hoặc mật khẩu", Toast.LENGTH_LONG).show();
        }
    }

    private void getListTaiKhoan() {
        LoginService.loginService.getListTaiKhoan()
                .enqueue(new Callback<List<TaiKhoan>>() {
                    @Override
                    public void onResponse(Call<List<TaiKhoan>> call, Response<List<TaiKhoan>> response) {
                        mTaiKhoans = response.body();
                        Log.d("listUser", mTaiKhoans.size() + "");
                    }

                    @Override
                    public void onFailure(Call<List<TaiKhoan>> call, Throwable t) {
                        Toast.makeText(LoginActivity.this, "Gọi API thất bại", Toast.LENGTH_LONG).show();
                        /*Log.d("erorr", call.toString() + "");
                        Log.d("erorr1", t.toString() + "");*/
                    }
                });
    }

    public static String md5(final String s) {
        final String MD5 = "MD5";
        try {
            // Create MD5 Hash
            MessageDigest digest = java.security.MessageDigest
                    .getInstance(MD5);
            digest.update(s.getBytes());
            byte messageDigest[] = digest.digest();

            // Create Hex String
            StringBuilder hexString = new StringBuilder();
            for (byte aMessageDigest : messageDigest) {
                String h = Integer.toHexString(0xFF & aMessageDigest);
                while (h.length() < 2)
                    h = "0" + h;
                hexString.append(h);
            }
            return hexString.toString();

        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        return "";
    }
}