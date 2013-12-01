package com.soul.soul;


import com.jjssm.Bluetooth.BluetoothService;
import com.jjssm.Bluetooth.DeviceListActivity;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends Activity {
	TextView stateOfLock;
	View forKeyboard, forMouse;
	boolean switchmode = true;
	Button getBtn;
	
	public static BluetoothService Bs = null;
	private BluetoothAdapter mBluetoothAdapter = null;
	
	
	
    public static final int MESSAGE_STATE_CHANGE = 1;
    public static final int MESSAGE_READ = 2;
    public static final int MESSAGE_WRITE = 3;
    public static final int MESSAGE_DEVICE_NAME = 4;
    public static final int MESSAGE_TOAST = 5;
	
    private Context context; 
    private int convertvalue = 32;
    
    private Button changebtn;
    private int idvalue;
    
	private static final int REQUEST_CONNECT_DEVICE_SECURE = 1;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		forKeyboard = findViewById(R.layout.keyboard);
		forMouse = findViewById(R.layout.mouse);
		
		stateOfLock = (TextView)findViewById(R.id.lockStateText);
				
		mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
		context = getApplicationContext();
	}
		
	public void sendMessage(String message) {
        // Check that we're actually connected before trying anything
        if (MainActivity.Bs.getState() != MainActivity.Bs.STATE_CONNECTED) {
            Toast.makeText(this, "non Connected", Toast.LENGTH_SHORT).show();
            return;
        }

        
        // Check that there's actually something to send
        if (message.length() > 0) {
            // Get the message bytes and tell the BluetoothChatService to write
        	String divideStr = "k\\";
        	divideStr += message;
            byte[] send = divideStr.getBytes();
            Log.d("send", ""+divideStr + "::::"+send.toString());
            MainActivity.Bs.write(send);
        }
    }
	
	private String readData = null;
	
	private String[] changeKeyArr ={
			"q",
			"w",
			"e",
			"r",
			"t",
			"y",
			"u",
			"i",
			"o",
			"p",
			"a",
			"s",
			"d",
			"f",
			"g",
			"h",
			"j",
			"k",
			"l",
			"z",
			"x",
			"c",
			"v",
			"b",
			"n",
			"m",			
	};
	
	private void transbtn(){
		for(int i = 0 ; i < changeKeyArr.length; i++){
			idvalue = context.getResources().getIdentifier( changeKeyArr[i], "id", this.getPackageName() );
			changebtn = (Button)findViewById(idvalue);
			
			/////////////////대소문자 변환/////////////////////////////////
			String changetxt = changebtn.getText().toString();
			
			char[] keycode = changetxt.toCharArray();
			if(keycode[0] >= 65 && keycode[0] <=90)
			{
				keycode[0] += convertvalue;
			}else{
				keycode[0] -= convertvalue;
			}
			
			changetxt = new String(keycode);
			/////////////////대소문자 변환//////////////////////////////////
			
			changebtn.setText(changetxt);
		}
	}
	
	public void onClick(View v) {		
		/*Button getBtn = null;
		kResult = (TextView) findViewById(R.id.result_keyboard);*/
		Intent it = null;
		getBtn = (Button) findViewById(v.getId());
		switch (v.getId()) {
		case R.id.to_mouse:
			it = new Intent(this , mouseActivity.class);
			startActivity(it);
			break;
		case R.id.capslock:
			transbtn();
			
			readData = getBtn.getText().toString();			
			sendMessage(readData);
			break;
		default:
			readData = getBtn.getText().toString();			
			sendMessage(readData);
			break;
		}

	}
	
	/*
	public void onClick(View v) {
		
		switch(v.getId())
		{
		case R.id.change_lock:
			if(switchmode == true)
			{
				switchmode = false;
				stateOfLock.setText("LOCk");
			}
			else
			{
				switchmode = true;		
				stateOfLock.setText("UNLOCk");
			}
			break;
			
		case R.id.to_keyboard:
			if(switchmode == false)
			{
				changePage(1);
			}
			break;
			
		case R.id.to_mouse:
			if(switchmode == false)
			{
				changePage(2);
			}	
			break;
			
		}
		
	}*/
	
	
	void changePage(int page)
	{
		forKeyboard.setVisibility(View.INVISIBLE);
		forMouse.setVisibility(View.INVISIBLE);
		
		switch(page)
		{
		case 1: // keyboard
			forKeyboard.setVisibility(View.VISIBLE);
			break;
		case 2: // mouse
			forMouse.setVisibility(View.VISIBLE);
			break;
		}
	}

	private final Handler mHandler = new Handler() {
        @Override
        public void handleMessage(Message msg) {            
        }
    };

    
    @Override
    public void onStart() {
        super.onStart();
            if (Bs == null) Bs = new BluetoothService(this, mHandler);
    }
    
	 @Override
	    public synchronized void onResume() {
	        super.onResume();       

	        if (Bs != null) {
	            // Only if the state is STATE_NONE, do we know that we haven't started already
	            if (Bs.getState() == Bs.STATE_NONE) {
	              // Start the Bluetooth chat services
	            	Log.d("seo","gogogo");
	            	Bs.start();
	            }
	        }
	    }
	
	    ///2
	    
	    private void connectDevice(Intent data, boolean secure) {
	        // Get the device MAC address
	        String address = data.getExtras()
	            .getString(DeviceListActivity.EXTRA_DEVICE_ADDRESS);
	        // Get the BluetoothDevice object
	        BluetoothDevice device = mBluetoothAdapter.getRemoteDevice(address);
	        // Attempt to connect to the device
	        Bs.connect(device, secure);
	    }

	    
	    public void onActivityResult(int requestCode, int resultCode, Intent data) {	        
	        switch (requestCode) {
	        case REQUEST_CONNECT_DEVICE_SECURE:
	            // When DeviceListActivity returns with a device to connect
	            if (resultCode == Activity.RESULT_OK) {
	                connectDevice(data, true);
	            }
	            break;	        	        
	        }
	    }
	    
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Intent serverIntent = null;
        switch (item.getItemId()) {
        case R.id.secure_connect_scan:
            serverIntent = new Intent(this, DeviceListActivity.class);
            startActivityForResult(serverIntent, REQUEST_CONNECT_DEVICE_SECURE);
            return true;
        case R.id.item1:
        	return true;
        }
        return false;
    }


}
