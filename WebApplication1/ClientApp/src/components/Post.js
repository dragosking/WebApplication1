import React, { Component } from 'react';

export class Post extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            text: '',
            apa: 'd',
            count: 0,
            values:[]
        };

    
    }


    handleInput() {

        fetch('api/SampleData/vader')
            .then(response => response.json())
            .then(data => {
                this.setState({ apa: data, loading: false });
            });

        this.setState({ text: 'vada' })
    }


    render() {

      

        let tagList = this.state.values.map((Tag,index) => {
            return (


                <div id={index} class="tag" onClick={this.selectTag}>{Tag.place}</div>

            );
        });

        return (
            <form>
                {this.handleInput}
                <h2>{this.state.apa}</h2>
              
            </form>
        );
    }
}