package com.soul.soul;

import java.util.*;

import com.soul.soul.R;

import android.app.*;
import android.os.*;
import android.util.Log;
import android.view.*;
import android.view.GestureDetector.*;
import android.widget.*;

public class mouseActivity extends Activity {
	ArrayList<String> arGesture = new ArrayList<String>(); 
	TextView mResult;
	GestureDetector mDetector;

	coordinateCalculation cc;
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.mouse);

		mResult = (TextView)findViewById(R.id.result_mouse_gesture);
		mDetector = new GestureDetector(this, mGestureListener);
		mDetector.setOnDoubleTapListener(mDoubleTapListener);
	}

	public boolean onTouchEvent(MotionEvent event) { 
		return mDetector.onTouchEvent(event); 
	}
	
	private boolean start = false;
	private int BackUp_x = 0;
	private int BackUp_y = 0;
	private int Up_x = 0;
	private int Up_y = 0;
	
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
    }
	OnGestureListener mGestureListener = new OnGestureListener() {
		

		public boolean onDown(MotionEvent e) {
			
			//BackUp_y = 0;
			AppendText(String.format("%d, %d", (int)e.getX(), (int)e.getY()));
			return false;
		}

		public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,	
				float velocityY) {
			/*AppendText(String.format("Fling : (%d,%d)-(%d,%d) (%d,%d)", 
					(int)e1.getX(), (int)e1.getY(), (int)e2.getX(), (int)e2.getY(), 
					(int)velocityX, (int)velocityY));*/
			
			
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
	
	public void onClick(View v){
		switch (v.getId()) {
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
			break;
		}
	}	
}