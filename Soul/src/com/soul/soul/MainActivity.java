package com.soul.soul;

import java.util.ArrayList;

import com.jjssm.Bluetooth.BluetoothService;
import com.jjssm.Bluetooth.DeviceListActivity;

import android.app.ActionBar;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.GestureDetector;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.GestureDetector.OnDoubleTapListener;
import android.view.GestureDetector.OnGestureListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends Activity{
	private TextView stateOfLock;
	private View forKeyboard, forMouse;
	private boolean switchmode = true; // true is Unlock
	private boolean KorEngSwitch = false; // true = kor, false = eng
	private Button getBtn;

	public static BluetoothService Bs = null;
	private BluetoothAdapter mBluetoothAdapter = null;

	// Message types sent from the BluetoothChatService Handler
	public static final int MESSAGE_STATE_CHANGE = 1;
	public static final int MESSAGE_READ = 2;
	public static final int MESSAGE_WRITE = 3;
	public static final int MESSAGE_DEVICE_NAME = 4;
	public static final int MESSAGE_TOAST = 5;

	private Context context;
	private int convertvalue = 32;

	private Button changebtn;
	private int idvalue;
	
	ArrayList<String> arGesture = new ArrayList<String>(); 
	TextView mResult;
	GestureDetector mDetector;

	private static final int REQUEST_CONNECT_DEVICE_SECURE = 1;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		forKeyboard = findViewById(R.id.keyboardInFramelayout);
		forMouse = findViewById(R.id.mouseInFramelayout);

		stateOfLock = (TextView) findViewById(R.id.lockStateText);

		mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
		context = getApplicationContext();

		forMouse.setVisibility(View.INVISIBLE);
		stateOfLock.setText("UNLOCK");
		
		mResult = (TextView)findViewById(R.id.result_mouse_gesture);
		mDetector = new GestureDetector(this, mGestureListener);
		mDetector.setOnDoubleTapListener(mDoubleTapListener);
		
		
	}
	
	public boolean onTouchEvent(MotionEvent event) { 
		return mDetector.onTouchEvent(event); 
	}
	/*
	public void sendMessage(String message) {
        // Check that we're actually connected before trying anything
        if (MainActivity.Bs.getState() != MainActivity.Bs.STATE_CONNECTED) {        	
            Toast.makeText(this, "non Connected", Toast.LENGTH_SHORT).show();        	
            return;
        }

        // Check that there's actually something to send
        if (message.length() > 0) {
            // Get the message bytes and tell the BluetoothChatService to write        	
            byte[] send = message.getBytes();           
            MainActivity.Bs.write(send);
        }
    }*/
	OnGestureListener mGestureListener = new OnGestureListener() {
		

		public boolean onDown(MotionEvent e) {
			//BackUp_y = 0;
			AppendText(String.format("%d, %d", (int)e.getX(), (int)e.getY()));
			return false;
		}

		public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,	
				float velocityY) {
			 /* AppendText(String.format("Fling : (%d,%d)-(%d,%d) (%d,%d)",
			 * (int)e1.getX(), (int)e1.getY(), (int)e2.getX(), (int)e2.getY(),
			 * (int)velocityX, (int)velocityY));
			 */
			AppendText(String.format("Fling : (%d,%d)-(%d,%d) (%d,%d)",
					(int) e1.getX(), (int) e1.getY(), (int) e2.getX(),
					(int) e2.getY(), (int) velocityX, (int) velocityY));
			
			
			return false;
		}

		public void onLongPress(MotionEvent e) {
	//		AppendText("LongPress");
		}

		public boolean onScroll(MotionEvent e1, MotionEvent e2,
				float distanceX, float distanceY) {		
			//int now_x = (int) e2.getX() - (int) e1.getX();
			//int now_y = (int) e2.getY() - (int) e1.getY();

			
			AppendText(String.format("m\\%d,%d", (int)distanceX, (int)distanceY ));
			sendMessage(String.format("m\\%d\n%d", -(int)distanceX , -(int)distanceY  ));

			return false;
		}
		
		

		public void onShowPress(MotionEvent e) {
	//		AppendText("ShowPress");
		}

		public boolean onSingleTapUp(MotionEvent e) {
//			AppendText("SingleTapUp");
			return false;
		}
	};

	OnDoubleTapListener mDoubleTapListener = new OnDoubleTapListener() {
		public boolean onDoubleTap(MotionEvent e) {
	//		AppendText("DoubleTap");
			return false;
		}

		public boolean onDoubleTapEvent(MotionEvent e) {
//			AppendText("DoubleTapEvent");
			return false;
		}

		public boolean onSingleTapConfirmed(MotionEvent e) {
//			AppendText("SingleTapConfirmed");
			return false;
		}
	};

	void AppendText(String text) {
		if (arGesture.size() > 10) {
			arGesture.remove(0);
		}
		arGesture.add(text);
		StringBuilder result = new StringBuilder();
		for (String s : arGesture) {
			result.append(s);
			result.append("\n");
		}
		mResult.setText(result.toString());
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
			Log.d("send", "" + divideStr + "::::" + send.toString());
			MainActivity.Bs.write(send);
		}
	}

	private String readData = null;

	private String[] changeKeyArr = { "q", "w", "e", "r", "t", "y", "u", "i",
			"o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x",
			"c", "v", "b", "n", "m", };

	private String[] changeKorKeyArr = { "ㅂ", "ㅈ", "ㄷ", "ㄱ", "ㅅ", "ㅛ", "ㅕ",
			"ㅑ", "ㅐ", "ㅔ", "ㅁ", "ㄴ", "ㅇ", "ㄹ", "ㅎ", "ㅗ", "ㅓ", "ㅏ", "ㅣ", "ㅋ",
			"ㅌ", "ㅊ", "ㅍ", "ㅠ", "ㅜ", "ㅡ", };

	private void transbtn() {
		for (int i = 0; i < changeKeyArr.length; i++) {
			idvalue = context.getResources().getIdentifier(changeKeyArr[i],
					"id", this.getPackageName());
			changebtn = (Button) findViewById(idvalue);

			// ///////////////대소문자 변환/////////////////////////////////
			String changetxt = changebtn.getText().toString();

			char[] keycode = changetxt.toCharArray();
			if (keycode[0] >= 65 && keycode[0] <= 90) {
				keycode[0] += convertvalue;
			} else {
				keycode[0] -= convertvalue;
			}

			changetxt = new String(keycode);
			// ///////////////대소문자 변환//////////////////////////////////

			changebtn.setText(changetxt);
		}
	}

	private void transKorbtn() {
		if (!KorEngSwitch) {
			hanChange(changeKorKeyArr);
			KorEngSwitch = true;
		} else {
			hanChange(changeKeyArr);
			KorEngSwitch = false;
		}
	}

	private void hanChange(String[] changearr) {
		for (int i = 0; i < changearr.length; i++) {
			idvalue = context.getResources().getIdentifier(changeKeyArr[i],
					"id", this.getPackageName());
			changebtn = (Button) findViewById(idvalue);

			changebtn.setText(changearr[i]);
		}
	}

	public void onClick(View v) {
		Intent it = null;
		getBtn = (Button) findViewById(v.getId());
		switch (v.getId()) {

		case R.id.change_lock:
			if (switchmode == true) {
				switchmode = false;
				stateOfLock.setText("LOCK");
			} else {
				switchmode = true;
				stateOfLock.setText("UNLOCK");
			}
			break;

		case R.id.to_keyboard:
			if (switchmode == true)
				changePage(1);
			break;

		case R.id.to_mouse:
			if (switchmode == true)
				changePage(2);
			break;

		case R.id.capslock:
			if (!KorEngSwitch) // 한글일때는 caps lock 무의미
			{
				transbtn();

				readData = getBtn.getText().toString();
				sendMessage(readData);
			}
			break;

		case R.id.kor_eng:
			transKorbtn();

			readData = getBtn.getText().toString();
			// sendMessage(readData);
			sendMessage("kor");
			break;
			
		case R.id.lclick:
			String lclick = "c\\lc";
			sendMessage(lclick);
			break;
		case R.id.rclick:
			String rclick = "c\\rc";
			sendMessage(rclick);
			break;
		case R.id.drag:
			Log.d("drag", "suc");
			String drag = "c\\drag";
			sendMessage(drag);
			break;

		default:
			readData = getBtn.getText().toString();
			sendMessage(readData);
			break;
		}

	}

	/*
	 * public void onClick(View v) {
	 * 
	 * switch(v.getId()) { case R.id.change_lock: if(switchmode == true) {
	 * switchmode = false; stateOfLock.setText("LOCk"); } else { switchmode =
	 * true; stateOfLock.setText("UNLOCk"); } break;
	 * 
	 * case R.id.to_keyboard: if(switchmode == false) { changePage(1); } break;
	 * 
	 * case R.id.to_mouse: if(switchmode == false) { changePage(2); } break;
	 * 
	 * }
	 * 
	 * }
	 */

	private void changePage(int page) {
		forKeyboard.setVisibility(View.INVISIBLE);
		forMouse.setVisibility(View.INVISIBLE);

		switch (page) {
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
		if (Bs == null)
			Bs = new BluetoothService(this, mHandler);
	}

	@Override
	public synchronized void onResume() {
		super.onResume();

		if (Bs != null) {
			// Only if the state is STATE_NONE, do we know that we haven't
			// started already
			if (Bs.getState() == Bs.STATE_NONE) {
				// Start the Bluetooth chat services
				Log.d("seo", "gogogo");
				Bs.start();
			}
		}
	}

	// /2

	private void connectDevice(Intent data, boolean secure) {
		// Get the device MAC address
		String address = data.getExtras().getString(
				DeviceListActivity.EXTRA_DEVICE_ADDRESS);
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
