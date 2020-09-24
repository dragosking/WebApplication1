import React, { Component } from 'react';


export class Latest extends Component {

    constructor(props) {
        super(props);
        this.state = {
            text: 'forst',
            apa: 'd',
            count: 0,
            values: [],
            forecasts: [],
            hmm:"ff",
            place: "va",
        };

        fetch('api/Index/LoadLatest')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data });
            });
       
    }

    componentDidMount(prevProps, prevState) {
     
          

        this.handleInput();
        
    }

    /*componentDidUpdate(prevProps, prevState) {
        if (prevProps.text != this.state.text) {
            this.setState({
                text:"yesss2"
            })
        }
    }*/


    handleInput() {

        fetch('api/Index/LoadLatest')
            .then(response => response.json())
            .then(data => {
                this.setState({ hmm: data,});
            });

    }




    render() {
        let list = this.state.forecasts.map((Tag, index) => {
            return (
                <div> {index}{ Tag}</div>

            );
        });

        return (
            <form>
                {list}
                {this.state.values}
              

            </form>
        );
    }



}
