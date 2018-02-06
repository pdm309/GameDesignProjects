import ddf.minim.*;

AudioPlayer player;
Minim minim;
int num_circles = 16;
String[] songnames = {"MACROSS 82-99 x YUNG BAE - Selfish High Heels (Feat. Harrison )","Roman - I Miss You (w/ MACROSS 82-99)"};
void setup() {
  frameRate(60);
  size(600,600);
  minim = new Minim(this);
  String[] sotengs = {"song1.mp3","song2.mp3"};
  
  player = minim.loadFile(songs[1], 2048);
  player.play();
}

int size = 0;
int score = 0;
int cooldown = 0;
int fireworksize = 0;
int fireworkspread = 59;
String alert = "";

void draw() {
  background(51, 102, 255);
  textSize(18);
  //text("Now Playing:",250,50);
  //text(songnames[0],10,100);
  //text(songnames[1],125,100);
  //text("Score: " + score, 25, 550);
  fill(204, 0, 0);
  stroke(255, 153, 0);
  ellipse(300, 450, 120, 120);
  fill(100);
  size+=2;
  fill(255-cooldown*2, 133+cooldown, 0+cooldown*2);
  ellipse(300, 450, size, size);
  
  pushMatrix();
  translate(300, 450);
  fill(255-cooldown*2, 133+cooldown, 0+cooldown*2);
  for (int i = 0; i < num_circles; i++) {
    rotate(radians(360/float(num_circles)));
    ellipse(fireworkspread, fireworksize, fireworksize, fireworksize);
  }
  popMatrix();
  
  if (size > 120){
    size = 0;
  }
  if (cooldown > 0){
    cooldown--;
    fireworksize++;
    fireworkspread+=2;
    //text(alert, 450, 550);
  }
  else if (cooldown < 0){
    cooldown++;
    //text(alert, 450, 550);
  }
  else {
    fireworksize = 0;
    fireworkspread = 59;
    //text(alert, 450, 550);
  }

  
  if (keyPressed == true && cooldown == 0){
    if (size == 120){
      //FRAME PERFECT
      score += 50;
      println("PERFECT! + 50");
      //text("PERFECT! + 50", 450, 550);
      alert = "PERFECT! + 50";
      println("Score: " + score);
      fireworksize = 0;
      fireworkspread = 59;
      cooldown = 30;
    }
    else if (size <= 20 || size >= 100){
      score += 10;
      println("Nice! + 10"); 
      //text("Nice! + 10", 450, 550);
      alert = "Nice! + 10";
      println("Score: " + score);
      fireworksize = 0;
      fireworkspread = 59;
      cooldown = 30;
    }
    else {
      println("Miss!");
      //text("Miss!", 450, 550);
      alert = "Miss!";
      cooldown = -120;
    }  
  }
}

void stop()
{
  player.close();
  minim.stop();
  super.stop();
}