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

    static listTable(values) {
        return (
            <table className='table table-striped'>
                <tbody>
                    {values.map(forecast =>
                        
                        <tr>
                            <td>
                                {forecast.a}
                            </td>
                            <td>
                                <input type="button" value="See Weather" onClick="" />
                            </td>

                        </tr>
                          
                    )}
                </tbody>
            </table>
        );
    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Post.listTable(this.state.values);

        return (
            <form>
               
                
                <input type='text' value={this.state.text} onChange={this.change} />
                <br />
                {contents}
            </form>
        );
    }
}