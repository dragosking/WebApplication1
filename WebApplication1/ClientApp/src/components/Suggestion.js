import React, { Component } from 'react';
import './Search.css';

import { Counter } from './Counter';
import { Route, withRouter } from 'react-router-dom';
import { Post } from './Post';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';


export class SuggestionBox extends Component {

    constructor(props) {
        super(props);
        this.state = {
            text: '',
            apa: 'd',
            va:'a',
            count: 0,
            values: [],
            change: 'noshow',
            changeSMHI: 'noshow',
            changeYR:'noshow',
            redirect:false
        };
        this.change = this.change.bind(this);
    }

    /*componentDidUpdate(prevProps, prevState) {
        const { history } = this.props;
   
            history.push('/post');
        
    }*/


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

        this.setState({
            text: p,
            count: this.state.values.length,
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
            values: [],
            change: 'noshow',
            redirect:true
        })
    }

    change(event) {

        this.handleInput(event.target.value);
    }


    selectTag = (e) => {
       // this.selectInput(this.state.values[e.target.id]);
        this.setState({
            apa: e.target.id,
            values: [],
            change: 'noshow',
            redirect: true,
            text:'',
        })
    }

 
    render() {

        let tagList = this.state.values.map((Tag, index) => {
        
            return (
                
                <table>
                    <tr className="rad">

                        <td className="lank">
                        
                            <Link id={index} to={
                                {
                                    pathname: "/post/" + Tag.place,
                                    state: {
                                        placeProps: Tag.place,
                                        latProps: Tag.coord.lat,
                                        lonProps: Tag.coord.lon,
                                        yrProps: Tag.detailYR,
                                        smhiProps: Tag.detailSMHI,
                                        daysProps: Tag.WeatherByDay
                                    }
                                }
                            } onClick={this.selectTag}>{Tag.place}</Link>
                          
                           
                        </td>
                        <td className="icon">

                            <div className='iconyr'> </div>
                           
                            <div className='iconsmhi'> </div>
                           
                         
                        </td>
                      


                    </tr>
                    <tr>
                        <div class="line"></div>
                     

                    </tr>
                    
                </table>
                
            );
        });

       

        return (
            <form>
                <input type='text' value={this.state.text} onChange={this.change} />
                <br />
               
                <div className={this.state.change}>
                    {tagList}
                </div>

            </form>
        );
    }
}

export default SuggestionBox
