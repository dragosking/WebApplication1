import React, { Component } from 'react';
import './Search.css';

export class SuggestionBox extends Component {

    constructor(props) {
        super(props);
        this.state = {
            text: '',
            apa: 'd',
            va:'',
            count: 0,
            values: [],
            change:'noshow'
        };

        this.change = this.change.bind(this);
    }


    handleInput(p) {

        if (p.length > 0) {
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

            this.setState({
                change:'bigblue'

            })
        } else {
            this.setState({
                values: [],
                change: 'noshow'
            })
        } 

       /* if (this.state.values >= 0) {

        }*/

       
        this.setState({
            text: p,
            count: p.length,
        })
    }

    selectInput(p) {

        

        fetch('api/SampleData/select',
            {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    place: p.place,
                    lat: p.lat,
                    lon: p.lon
                })
            });

        this.setState({
            apa: p.place,
            values: []
        })
    }

    change(event) {

        //this.setState({ text: event.target.value })

        this.handleInput(event.target.value);
    }


    selectTag = (e) => {
        this.selectInput(this.state.values[e.target.id]);
    }

   

    render() {

        let tagList = this.state.values.map((Tag, index) => {
            return (

                /*<select id={index} class="tag" onClick={this.selectTag}>
                    {tag.place}
                </select>*/

                <div id={index} class="tag" onClick={this.selectTag}>{Tag.place}</div>

            );
        });

         

        return (
            <form>

                {this.state.count}
                <input type='text' value={this.state.text} onChange={this.change} />
                <br />

                <div className={this.state.change}>
                    {tagList}
                </div>
            </form>
        );
    }



}

