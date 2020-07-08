jQuery(document).ready(function ($) {

  $(".stars").mouseover(function(){
      var current = $(this);
      $(".stars").each(function(index){
          $(this).addClass("hovered-stars");
          if(index == current.index())
          {
              return false;
          }
      });
  });
  $(".stars").mouseleave(function(){
      $(".stars").removeClass("hovered-stars");
  });
  $(".stars").click(function(){
      $(".stars").removeClass("clicked-stars");
      $(".hovered-stars").addClass("clicked-stars");
      $("#message").html("You have rated this with " + 
          $(".clicked-stars").length + " stars.")
  });
});