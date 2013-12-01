package com.soul.soul;

import java.io.Console;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.soul.soul.R;

public class keyboardActivity extends Activity {

	TextView kResult;


	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.keyboard);

	}
	
	private void sendMessage(String message) {
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

	
	public void onClick(View v) {
		Button getBtn = null;
		kResult = (TextView) findViewById(R.id.result_keyboard);
		
		switch (v.getId()) {

		// ///////Lock State/////////////
		case R.id.num_loack_state:
		case R.id.caps_loack_state:
		case R.id.scroll_loack_state:

			// ///////arrow key/////////////
		case R.id.up_click:
		case R.id.right_click:
		case R.id.down_click:
		case R.id.left_click:

			// ///////other func/////////////
		case R.id.pst_sc_sys_rq:
		case R.id.scroll_lock:
		case R.id.pause_break:

		case R.id.insert:
		case R.id.home:
		case R.id.page_up:
		case R.id.delete:
		case R.id.end:
		case R.id.page_down:

			// ///////right num/////////////
		case R.id.numLock:
		case R.id.division:
		case R.id.multiplication:
		case R.id.minus:
		case R.id.seven:
		case R.id.eight:
		case R.id.nine:
		case R.id.four:
		case R.id.five:
		case R.id.six:
		case R.id.one:
		case R.id.two:
		case R.id.three:
		case R.id.zero:
		case R.id.delete2:
		case R.id.plus:
		case R.id.enter2:

			// ///////function/////////////
		case R.id.esc:
		case R.id.f1:
		case R.id.f2:
		case R.id.f3:
		case R.id.f4:
		case R.id.f5:
		case R.id.f6:
		case R.id.f7:
		case R.id.f8:
		case R.id.f9:
		case R.id.f10:
		case R.id.f11:
		case R.id.f12:

		case R.id.point:
		case R.id.num1:
		case R.id.num2:
		case R.id.num3:
		case R.id.num4:
		case R.id.num5:
		case R.id.num6:
		case R.id.num7:
		case R.id.num8:
		case R.id.num9:
		case R.id.num0:
		case R.id.bar:
		case R.id.equal:
		case R.id.backslash:
		case R.id.back:

		case R.id.tab:
		case R.id.q:
		case R.id.w:
		case R.id.e:
		case R.id.r:
		case R.id.t:
		case R.id.y:
		case R.id.u:
		case R.id.i:
		case R.id.o:
		case R.id.p:
		case R.id.square_bracket_left:
		case R.id.square_bracket_right:
		case R.id.enter_:

		case R.id.caps_lock:
		
			break;
		case R.id.a:
		case R.id.s:
		case R.id.d:
		case R.id.f:
		case R.id.g:
		case R.id.h:
		case R.id.j:
		case R.id.k:
		case R.id.l:
		case R.id.semicolon:
		case R.id.quotation_marks:
		case R.id.enter:

		case R.id.shift:
		case R.id.z:
		case R.id.x:
		case R.id.c:
		case R.id.v:
		case R.id.b:
		case R.id.n:
		case R.id.m:
		case R.id.comma:
		case R.id.dot:
		case R.id.slash:
		case R.id.shift2:

		case R.id.ctrl:
		case R.id.windows:
		case R.id.alt:
		case R.id.chinese_character:
		case R.id.space:
		case R.id.kor_eng:
		case R.id.alt2:
		case R.id.windows2:
		case R.id.mouse_right_click:
		case R.id.ctrl2:
			Log.d("send", "succc");
			sendMessage("o");
			
			/*getBtn = (Button) findViewById(v.getId());
			kResult.setText(getBtn.getText());*/
			break;
		}

	}

}
