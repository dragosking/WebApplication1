import React, { Component } from 'react';
import { SuggestionBox } from './Suggestion';

export class Test extends Component {

    constructor(props) {
        super(props);
        this.state = {
            text: '',
            apa: 'd',
            count: 0,
            values: []
        };

        this.change = this.change.bind(this);
    }


    handleInput(p) {

        fetch('api/SampleData/test',
            {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ a: p })
            }).
            then(response => response.json())
            .then(data => {
                this.setState({ values: data, loading: false });
            });

        this.setState({ text: p })
    }

 
    change(event) {

        //this.setState({ text: event.target.value })

        this.handleInput(event.target.value);
    }

    render() {



        return (
            <form>

                {this.state.text}
                <input type='text' value={this.state.text} onChange={this.change} />
                
                <br />

                

            </form>
        );
    }



}
