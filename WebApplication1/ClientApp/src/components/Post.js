import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { createHashHistory } from 'history'

export const historyr = createHashHistory()

export class Post extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            r: 'ddsadadas',
            t: this.props.location.myCustomProps,
            values:[]
        };

        fetch('api/SampleData/select',
            {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ place: this.state.t })
            }).
            then(response => response.json())
            .then(data => {
                this.setState({ values: data, loading: false });
            });

        
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps.location.myCustomProps !== this.props.location.myCustomProps) {
            this.setState({
                t: this.props.location.myCustomProps
            })

            fetch('api/SampleData/select',
                {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        place: this.props.location.myCustomProps,
        
                    })
                });

          
        }
    }

    selectTag2 = (e) => {
        // this.selectInput(this.state.values[e.target.id]);
        this.setState({

            r:'finiad',
        })
    }

     change() {

        this.setState({

            r: 'finiad',
        })
    }


    render() {

        return (
            <form>

                <h1>{this.state.t}</h1>
                
            </form>
        );
    }
}

//export default withRouter(Post);