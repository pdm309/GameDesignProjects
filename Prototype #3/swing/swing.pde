float gravity = 0.4f;

class Swing {
  PVector pivot;
  float armLength;
  float bobRad;

  float angle;
  float aVel;
  float aAcc;
  
  float startX, startY;
  boolean isTethered = true;
  float r = random(-400, 400);
  Swing() {
    reset();
  }
    
  void update() {
    if (keyPressed == true && isTethered){
      isTethered = false;
      startX = cos(angle) * armLength;
      startY = sin(angle) * armLength;
    }
    
    if (!isTethered){
      
    }
    else {
      aAcc = (-gravity / armLength) * sin(angle);
      aVel += aAcc;
      angle += aVel;
      float y = cos(angle) * armLength;
      aVel *= 0.999f;
    }
  }

  void reset() {
    angle = PI/2;
    pivot = new PVector(0, 0);
    aVel = 0.01f;
    armLength = height/2;
    bobRad = 25;
    isTethered = true;
    gravity = 0.4f;
    r = random(-400, 400);
    print(r + "\n");
  }

  void draw() {
    float x = cos(angle) * armLength;
    float y = sin(angle) * armLength;

    stroke(0);
    fill(150,0,255);
    // Note, flipping x and y so the pendulum doesn't swing along the x-axis.
    if (!isTethered){
      ellipse(startY, startX, bobRad*2, bobRad*2);
      if (angle < 0){
        startY += aVel*50 + aAcc;
        startX += 20*gravity;
        if (aVel < 0.0002){
          aVel+=.0002;
        }
        else {
          aAcc = 0;
          aVel = 0;
        }
      }
      else {
        startY += aVel*50 + aAcc;
        startX += 20*gravity;
        if (aVel > 0.0002){
          aVel-=.0002;
        }
        else {
          aAcc = 0;
          aVel = 0;
        }
        

      }
      if (startX > height - 50){
        print (startY);
        print ("\n");
        print (r-150);
        print ("\n");
        print (r+150);
        print ("\n");
        if (startY > r - 125 && startY < r + 150){
          gravity = 0;
        }
        else {
         gravity = 0.4f; 
        }
      }
    }
    else{
      line(pivot.x, pivot.y, y, x);
      ellipse(y, x, bobRad*2, bobRad*2);
      //rotate(PI/4.0);
      rect(pivot.x, pivot.y, 20, 20);
    }
    
    //rotate(-PI/4.0);
    fill(244,241,66);
    rect(r, height - 25, 50, 50);
    fill(244,194,66);
    rect (r - 50, height - 25, 50, 50);
    rect (r + 50, height - 25, 50, 50);
    fill (244, 98, 66);
    rect (r - 100, height - 25, 50, 50);
    rect (r + 100, height - 25, 50, 50);
    
  }
}

Swing s;

void setup() {
  size(1200, 800);
  frameRate(60);
  strokeWeight(2);
  s = new Swing();
}

void draw() {
  background(100);
  pushMatrix();
  translate(width/2, 0);
  s.update();
  s.draw();
  popMatrix();
}

void mousePressed() {
  s.reset();
}