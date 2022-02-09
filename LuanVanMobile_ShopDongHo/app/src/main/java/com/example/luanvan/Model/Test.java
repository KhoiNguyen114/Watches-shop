package com.example.luanvan.Model;

import com.google.gson.annotations.SerializedName;

public class Test {
    @SerializedName("title")
    String title;
    @SerializedName("url")
    String url;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }
}
