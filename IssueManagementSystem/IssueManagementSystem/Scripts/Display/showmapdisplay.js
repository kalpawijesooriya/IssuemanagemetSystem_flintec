
var jsonText = document.getElementById("map").textContent;


function Shape(x, y, w, h, fill, machine) {
    this.x = x || 0;
    this.y = y || 0;
    this.w = w || 1;
    this.h = h || 1;
    this.fill = fill || '#AAAAAA';
    this.machine = machine || 'NULL*';
}


Shape.prototype.draw = function (ctx1) {
    ctx1.fillStyle = this.fill;
    ctx1.fillRect(this.x, this.y, this.w, this.h);

}


Shape.prototype.contains = function (mx, my) {
    return (this.x <= mx) && (this.x + this.w >= mx) &&
        (this.y <= my) && (this.y + this.h >= my);
}

function CanvasState(canvas) {

    this.canvas = canvas;
    this.width = canvas.width;
    this.height = canvas.height;
    this.ctx = canvas.getContext('2d');

    var stylePaddingLeft, stylePaddingTop, styleBorderLeft, styleBorderTop;
    if (document.defaultView && document.defaultView.getComputedStyle) {
        this.stylePaddingLeft = parseInt(document.defaultView.getComputedStyle(canvas, null)['paddingLeft'], 10) || 0;
        this.stylePaddingTop = parseInt(document.defaultView.getComputedStyle(canvas, null)['paddingTop'], 10) || 0;
        this.styleBorderLeft = parseInt(document.defaultView.getComputedStyle(canvas, null)['borderLeftWidth'], 10) || 0;
        this.styleBorderTop = parseInt(document.defaultView.getComputedStyle(canvas, null)['borderTopWidth'], 10) || 0;
    }

    var html = document.body.parentNode;
    this.htmlTop = html.offsetTop;
    this.htmlLeft = html.offsetLeft;



    this.valid = false; // when set to false, the canvas will redraw everything
    this.shapes = [];  // the collection of things to be drawn
    this.shapes = createObjectArray(jsonText);
    this.dragging = false; // Keep track of when we are dragging
    // the current selected object. In the future we could turn this into an array for multiple selection
    this.selection = null;
    this.dragoffx = 0; // See mousedown and mousemove events for explanation
    this.dragoffy = 0;

    var myState = this;

    //fixes a problem where double clicking causes text to get selected on the canvas
    canvas.addEventListener('selectstart', function (e) { e.preventDefault(); return false; }, false);
    // Up, down, and move are for dragging
    canvas.addEventListener('mousedown', function (e) {
        var mouse = myState.getMouse(e);
        var mx = mouse.x;
        var my = mouse.y;
        var shapes = myState.shapes;
        var l = shapes.length;
        for (var i = l - 1; i >= 0; i--) {
            if (shapes[i].contains(mx, my)) {
                var mySel = shapes[i];
                myState.selection = mySel;
                myState.valid = false;
               
                return;
            }
        }

        if (myState.selection) {
            myState.selection = null;
            myState.valid = false; // Need to clear the old selection border
        }
    }, true);

    canvas.addEventListener('mousemove', function (e) {
        if (myState.dragging) {
            var mouse = myState.getMouse(e);
            myState.selection.x = mouse.x - myState.dragoffx;
            myState.selection.y = mouse.y - myState.dragoffy;
            myState.valid = false; // Something's dragging so we must redraw

        }
    }, true);


    canvas.addEventListener('mouseup', function (e) {
        myState.dragging = false;
    }, true);

    setInterval(function () {

        for (var i = 0; i < myState.shapes.length; i++) {

            var mySelx = myState.shapes[i];

            for (var k = 0; k < blinking_machines.length; k++) {
                if (myState.shapes[i].machine == blinking_machines[k]) {

                    if (globalvariable == 1) {
                        //  console.log("blinkColor" + globalvariable);
                        myState.ctx.strokeStyle = myState.blinkColor;
                        myState.ctx.lineWidth = myState.selectionWidth;
                        myState.ctx.strokeRect(mySelx.x, mySelx.y, mySelx.w, mySelx.h);

                    }
                    if (globalvariable == 0) {
                        // console.log("selectionColor" + globalvariable);
                        myState.ctx.strokeStyle = myState.selectionColor;
                        myState.ctx.lineWidth = myState.selectionWidth;
                        myState.ctx.strokeRect(mySelx.x, mySelx.y, mySelx.w, mySelx.h);

                    }
                }
            }
        }
    }, 10);


    this.blinkColor = '#ff4000';
    this.selectionColor = '#f4dc42';
    this.selectionWidth = 5;
    this.interval = 30;
    setInterval(function () { myState.draw(); }, myState.interval);
}

CanvasState.prototype.addShape = function (shape) {
    this.shapes.push(shape);
    this.valid = false;
}

CanvasState.prototype.clear = function () {
    this.ctx.clearRect(0, 0, this.width, this.height);
}

CanvasState.prototype.draw = function () {
    if (!this.valid) {
        var ctx = this.ctx;
        var shapes = this.shapes;
        this.clear();

        var l = shapes.length;
        for (var i = 0; i < l; i++) {
            var shape = shapes[i];
            // We can skip the drawing of elements that have moved off the screen:
            if (shape.x > this.width || shape.y > this.height ||
                shape.x + shape.w < 0 || shape.y + shape.h < 0) continue;
            shapes[i].draw(ctx);
        }


        if (this.selection != null) {
            ctx.strokeStyle = this.selectionColor;
            ctx.lineWidth = this.selectionWidth;
            var mySel = this.selection;
            ctx.strokeRect(mySel.x, mySel.y, mySel.w, mySel.h);
        }



        this.valid = true;
    }
}


CanvasState.prototype.getMouse = function (e) {
    var element = this.canvas, offsetX = 0, offsetY = 0, mx, my;
    // Compute the total offset
    if (element.offsetParent !== undefined) {
        do {
            offsetX += element.offsetLeft;
            offsetY += element.offsetTop;
        } while ((element = element.offsetParent));
    }

    offsetX += this.stylePaddingLeft + this.styleBorderLeft + this.htmlLeft;
    offsetY += this.stylePaddingTop + this.styleBorderTop + this.htmlTop;

    mx = e.pageX - offsetX;
    my = e.pageY - offsetY;

    return { x: mx, y: my };
}
globalvariable = 0;

function init() {
    var s = new CanvasState(document.getElementById('canvas1'));

    setInterval(function (){
        //console.log(globalvariable);
        if (globalvariable == 0) { globalvariable = 1; }
        else { globalvariable = 0; }
    }, 100);

    var xa = ["Machine 10", "Machine 11", "Machine 12", "Machine 13"];
    set_blinking_machines(xa);
}


function createObjectArray(jsonText) {

    console.log(jsonText);
    var arr = JSON.parse(jsonText);
    console.log(arr);
    var newArray = new Array();

    for (var i = 0; i < arr.length; i++)
        {
            newArray.push(new Shape(arr[i].x, arr[i].y, arr[i].w, arr[i].h, arr[i].fill, arr[i].machine));
    }

    console.log(newArray);
    return newArray;
}

window.onload = init;
blinking_machines;

function set_blinking_machines(blinking_machines_A) {
    blinking_machines = blinking_machines_A;
}