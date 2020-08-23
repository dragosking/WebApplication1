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

    handleAddItem = (value) => {
        this.setState({
            apa: value,
         
        })
    }

    change(event) {
      
        //this.setState({ text: event.target.value })

        this.handleInput(event.target.value);
    }


    selectTag = (e) => {
        this.setState(
            {
                apa: e.target.id,
                values: []
            }
        );
    }


    render() {

      

        let tagList = this.state.values.map((Tag) => {
            return (

                <div id={Tag.a} class="tag" onClick={this.selectTag}>{Tag.a}</div>

            );
        });

        return (
            <form>

                <h2>{this.state.apa}</h2>
                <input type='text' value={this.state.text} onChange={this.change} />
                <br />
                {tagList}
            </form>
        );
    }
}