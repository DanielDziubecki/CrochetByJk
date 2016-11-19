const {Carousel} = ReactBootstrap;

var MyCarousel = React.createClass({
    render: function () {
        return (
       <Carousel>
         <Carousel.Item>
           <img width={540} height={300} alt="540x300" src="/Resources/Images/1.jpg"/>
           <Carousel.Caption>
             <h3>First slide label</h3>
             <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
           </Carousel.Caption>
         </Carousel.Item>
         <Carousel.Item>
           <img width={540} height={300} alt="540x300" src="/Resources/Images/2.jpg"/>
           <Carousel.Caption>
             <h3>Second slide label</h3>
             <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
           </Carousel.Caption>
         </Carousel.Item>
         <Carousel.Item>
           <img width={540} height={300} alt="540x300" src="/Resources/Images/3.jpg"/>
           <Carousel.Caption>
             <h3>Third slide label</h3>
             <p>Praesent commodo cursus magna, vel scelerisque nisl consectetur.</p>
           </Carousel.Caption>
         </Carousel.Item>
       </Carousel>);
    }
});
ReactDOM.render(<MyCarousel/>, document.getElementById('carousel'));
