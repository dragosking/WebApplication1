import React, { Component } from 'react';

export class Post extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            text: '',
            apa: '',
            count: 0,
            values:[]
        };

        this.change = this.change.bind(this);
    }


    handleInput(p) {

        var PL = {
            a: 1,
            b: 2
        };

        var data = new FormData();
        data.append("PoLos", JSON.stringify(PL));

        fetch('api/SampleData/test',
            {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ a: p })
            }).
            then(response => response.text())
            .then(data => {
                this.setState({ values: data, loading: false });
            });

        this.setState({ text: p })
    }

    change(event) {
        this.setState({ count: this.state.count + 1 });
        //this.setState({ text: event.target.value })

        this.handleInput(event.target.value);
    }

    static listTable(values) {
        return (
            <table className='table table-striped'>
                <tbody>
                    {values.map(forecast =>
                        
                            <td>{forecast.a}</td>
                          
                    )}
                </tbody>
            </table>
        );
    }

    render() {

        /*let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Post.listTable(this.state.values);*/

        return (
            <form>
                <h1>{this.state.text}</h1>
                <h2>{this.state.apa}</h2>
                <h3>{this.state.count}</h3>
               
                
                <input type='text' value={this.state.text} onChange={this.change} />
                <br />
                {Post.listTable(this.state.values)}
            </form>
        );
    }
}